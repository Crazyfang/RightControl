using System.Linq;
using Newtonsoft.Json;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordTrancation;
using RightControl.WebApp.Areas.Admin.Controllers;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using RightControl.Common;
using RightControl.IRepository.RecordTrancation;
using RightControl.IService;

namespace RightControl.WebApp.Areas.RecordTrancation.Controllers
{
    public class RecordBorrowController : BaseController
    {
        public IFileListService FileListService { get; set; }
        public IRecordFileTypeService RecordFileTypeService { get; set; }
        public ISelectTypeService selectTypeService { get; set; }
        public IRecordService recordService { get; set; }
        public IApplyBorrowService ApplyBorrowService { get; set; }
        public IApplyBorrowFileListService ApplyBorrowFileListService { get; set; }
        public IBorrowHistoryService BorrowHistoryService { get; set; }
        public IContractFileTypeService ContractFileType { get; set; }
        public IOperateLogService OperateLogService { get; set; }
        public IApplyCopyService ApplyCopyService { get; set; }


        public IApplyCopyFileListService ApplyCopyFileListService { get; set; }
        public IUserService UserService { get; set; }
       

        // GET: RecordTrancation/RecordBorrow
        public ActionResult Hand()
        {
            return View("../RecordOperation/RecordAdd");
        }

        /// <summary>
        /// 待移交档案清单
        /// </summary>
        /// <param name="filter">过滤条目</param>
        /// <param name="pageInfo">页码、条目数量等信息</param>
        /// <returns></returns>
        public ActionResult RecordListHandOver(Record filter, PageInfo pageInfo)
        {
            filter.RecordStatus = 1;
            var result = recordService.GetListByFilter(filter, pageInfo);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult RecordListBorrow(ApplyBorrowTable filter, PageInfo pageInfo)
        {
            //申请借阅待审核-1，审核通过-2，审核未通过-3
            filter.ApplyState = Request["ApplyState"].ToInt();
            var result = ApplyBorrowService.GetListByFilter(filter, pageInfo);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult RecordHandOverCheck(string recordId)
        {
            var record = recordService.GetByWhere($" where RecordID='{recordId}'").ToList()[0];

            var operate = new OperateLog()
            {
                RecordId = recordId,
                OperateTime = DateTime.Now,
                OperatePeople = Operator.RealName,
                OperateType = "移交确认",
                OperateInfo = $"移交确认 档案号:{recordId},档案用户姓名:{record.RecordUserName},档案用户客户号:{record.RecordUserCode}"
            };

            OperateLogService.CreateModel(operate);

            //档案状态说明:1-待移交  2-在库   3-借阅中
            record.RecordStatus = 2;
            record.RecordStorageLoc = Request["storageLoc"];
            record.HandOverTime = DateTime.Now;
            var result = recordService.UpdateModel(record) ? SuccessTip() : ErrorTip();

            return Json(result);
        }

        public ActionResult RecordBorrowReturn(string borrowId)
        {
            //var result = BorrowHistoryService.ReturnBorrowFile(borrowId);
            var recordList = ApplyBorrowFileListService.GetByWhere(" where BorrowID=@BorrowID", new { BorrowID = borrowId }).ToList();
            var apply = ApplyBorrowService.GetByWhere(" where BorrowID=@BorrowID", new {BorrowID = borrowId}).First();
            apply.ApplyState = 4;
            ApplyBorrowService.UpdateModel(apply);
            //if(result)
            //{
                foreach(var item in recordList)
                {
                    var operate = new OperateLog()
                    {
                        RecordId = item.RecordID,
                        OperatePeople = Operator.RealName,
                        OperateTime = DateTime.Now,
                        OperateType = "借阅档案归还确认",
                        OperateInfo = $"外借档案借阅归还 档案号:{item.RecordID}"
                    };
                    var record = recordService.GetByWhere(" where RecordID=@RecordID", new {RecordID = item.RecordID})
                        .First();
                    record.RecordStatus = 2;
                    recordService.UpdateModel(record);
                    OperateLogService.CreateModel(operate);

                }
                return Json(SuccessTip("外借档案归还成功!"));
            //}
            //else
            //{
                //return Json(ErrorTip("外借档案归还失败，请重试!"));
            //}
        }

        public ActionResult ApplyBorrow(string recordIdString, string borrowOnly)
        {
            var recordIdList = recordIdString.Split(',').ToList();
            var failBorrowList = new List<string>();

            for (var i = recordIdList.Count - 1; i >= 0; i--)
            {
                var record = recordService.GetRecord(recordIdList[i]);
                if (record.RecordStatus != 2)
                {
                    failBorrowList.Add(recordIdList[i]);
                    recordIdList.RemoveAt(i);
                }
                else
                {
                    if (string.IsNullOrEmpty(borrowOnly) && record.RecordManager != Operator.UserName)
                    {
                        failBorrowList.Add(recordIdList[i]);
                        recordIdList.RemoveAt(i);
                    }
                }
            }

            var timeNow = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var apply = new ApplyBorrowTable()
            {
                BorrowID = timeNow,
                ApplyTime = DateTime.Now,
                ApplyUser = Operator.UserName,
                ApplyState = 1
            };

            var message = new StringBuilder();
            if (failBorrowList.Any())
            {
                message.Append($"  {string.Join(",", recordIdList)}申请借阅失败,失败原因:档案不在库或者不归属您所在的支行!");
            }

            if (recordIdList.Any())
            {
                message.Append($"{string.Join(",", recordIdList)}申请借阅成功");

                var result = ApplyBorrowService.ApplyBorrow(timeNow, apply, recordIdList, Operator.RealName);
                if (result)
                {
                    foreach (var item in recordIdList)
                    {
                        var operate = new OperateLog()
                        {
                            RecordId = item,
                            OperatePeople = Operator.RealName,
                            OperateType = "申请借阅",
                            OperateTime = DateTime.Now,
                            OperateInfo = $"申请借阅 借阅号:{timeNow},档案编号:{item}"
                        };
                        OperateLogService.CreateModel(operate);
                    }
                    return Json(SuccessTip($"{message.ToString()},请等待审核!"));
                }
                else
                {
                    return Json(ErrorTip("申请借阅失败,请重试!"));
                }
            }
            else
            {
                return Json(ErrorTip("申请借阅失败!"));
            }
           
        }

        public ActionResult ApplyBatchBorrow()
        {
            var timeNow = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var apply = new ApplyBorrowTable()
            {
                BorrowID = timeNow,
                ApplyTime = DateTime.Now,
                ApplyUser = Operator.UserName
            };
            var result = ApplyBorrowService.ApplyBatchBorrow(timeNow, apply, Operator.DepartmentCode, Operator.RealName);

            if(result)
            {
                return Json(SuccessTip("申请批量借阅成功，请等待审核!"));
            }
            else
            {
                return Json(ErrorTip("申请批量借阅失败,请重试!"));
            }
        }

        public ActionResult PrintPage(string recordId)
        {
            var record = recordService.GetRecord(recordId);
            var typeList = recordService.GetTheTypeList(recordId);
            var fileList = recordService.GetRecordListByRecordId(recordId);
            var otherFileList = recordService.GetOtherFileListByRecordId(recordId);
            var html = "";

            foreach (var type in typeList)
            {
                var count = 1;
                html += "<div style='width: auto; page-break-after:always; text-align: center'>"+
                        "<p style='font-size:50px;color: black;'>"+ type.RecordTypeName +"</p>";
                if (type.RecordTypeName.Contains("权证") || type.RecordTypeName.Contains("要件"))
                {
                    html += $"<p style='text-align: right; margin-right: 200px'>合同号:{type.HoldingCell}</p>";
                }

                var col = (type.RecordTypeName == "个人管理类" || type.RecordTypeName == "担保企业管理类" ||
                           type.RecordTypeName == "个人担保管理类")
                    ? 5
                    : 4;

                html += "<table border=\"1\" cellspacing=\"0\">" +
                        "<tr>" +
                        "<th rowspan=\"" + col + "\">编号</th>" +
                        "<th colspan=\"3\">移交事由:</th>" +
                        "<th colspan=\"7\">新增客户(√)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;存量周转(&nbsp; &nbsp; &nbsp; &nbsp;)</th>" +
                        "</tr>" +
                        "<tr>" +
                        "<th colspan=\"3\">客户经理:</th>" +
                        "<th colspan=\"4\">"+ record.RecordManagerName +"</th>" +
                        "<th colspan=\"1\"> 移交日:</th>" +
                        "<th colspan=\"2\">"+ DateTime.Today +"</th>" +
                        "</tr>" +
                        "<tr>" +
                        "<th colspan=\"3\">客户名称:</th>" +
                        "<th colspan=\"4\">"+ record.RecordUserName +"</th>" +
                        "<th colspan=\"1\">支行:</th>" +
                        "<th colspan=\"2\">"+ record.RecordDepartmentName +"</th>" +
                        "</tr>";

                if (col == 5)
                {
                    html += "<tr>" +
                            "<th colspan=\"3\">保证人名称:</th>" +
                            $"<th colspan=\"7\">{type.HoldingCell}</th>" +
                            "</tr>";
                }

                html += "<tr>" +
                        "<th colspan=\"3\">资料名称:</th>"+
                        "<th colspan=\"1\">张数</th>"+
                        "<th colspan=\"1\">是否原件</th>"+
                        "<th colspan=\"2\">放款中心确认</th>"+
                        "<th colspan=\"3\">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注</th>"+
                        "</tr>";

                foreach (var temp in fileList.Where(item => item.RecordType == type.ID))
                {
                    html += "<tr>" +
                            "<th>"+ count + "</th>" +
                            "<th colspan=\"3\">"+ temp.RecordFileName + "</th>" +
                            "<th>"+ temp.Amount +"</th>" +
                            "<th></th>" +
                            "<th colspan=\"2\"></th>" +
                            "<th colspan=\"3\"></th>" +
                            "</tr>";

                    count += 1;
                }

                foreach (var temp in otherFileList.Where(item => item.RecordFileType == type.ID))
                {
                    html += "<tr>" +
                            "<th>" + count + "</th>" +
                            "<th colspan=\"3\">" + temp.FileName + "</th>" +
                            "<th>"+ temp.Amount +"</th>" +
                            "<th></th>" +
                            "<th colspan=\"2\"></th>" +
                            "<th colspan=\"3\"></th>" +
                            "</tr>";

                    count += 1;
                }

                if (count < 35)
                {
                    for (int i = 0; i < 35 - count; i++)
                    {
                        html += "<tr><th>&nbsp;</th>" +
                                "<th colspan=\"3\"></th>" +
                                "<th></th>" +
                                "<th></th>" +
                                "<th colspan=\"2\"></th>" +
                                "<th colspan=\"3\"></th></tr>";
                    }
                }
                
                html += "</table>"+
                        "<p>"+
                        "支行移交人签字:"+
                        "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"+
                        "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;"+
                        "放款中心审查人签字:"+
                        "</p>"+
                        "</div>";
            }

            ViewBag.html = html;
            
            return View();
        }

        public ActionResult HandOverCheck(string recordId)
        {
            ViewBag.recordId = recordId;
            return View();
        }

        public ActionResult ReturnListDetail(string borrowId)
        {
            var borrowRecordList = ApplyBorrowFileListService
                .GetByWhere(" where BorrowID=@BorrowID", new {BorrowID = borrowId}).Select(item => item.RecordID)
                .ToList();

            var html = "";
            foreach (var recordId in borrowRecordList)
            {
                var record = recordService.GetRecord(recordId);
                var typeList = ContractFileType.GetListByRecordId(recordId);
                var fileList = recordService.GetRecordListByRecordId(recordId);
                var otherFileList = recordService.GetOtherFileListByRecordId(recordId);

                html += "<fieldset class='layui-elem-field layui-field-title' style='margin-top:20px;'>" +
                        "<legend>" + record.RecordUserName +"</legend>" +
                        "</fieldset><div style='padding: 20px; background-color: #F2F2F2;'>";
                foreach (var type in typeList)
                {
                    html += "<div class=\"layui-card\">" +
                            "<div class=\"layui-card-header\">" + type.RecordTypeName + " "+ type.HoldingCell +"</div>" +
                            "<div class=\"layui-card-body\">" +
                            "<div class=\"layui-fluid\">";

                    foreach (var temp in fileList.Where(item => item.RecordType == type.ID))
                    {
                        var time = temp.ExpirationTime == null
                            ? "无过期时间"
                            : temp.ExpirationTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                        html += "<div class=\"layui-row\" style=\"text-align: center;\">" +
                                "<div class=\"layui-col-xs4\">" + temp.RecordFileName + "</div>" +
                                "<div class=\"layui-col-xs4\">" + time + "</div>" +
                                "<div class=\"layui-col-xs4\">x" + temp.Amount + "</div>" +
                                "</div>";
                    }

                    foreach (var temp in otherFileList.Where(item => item.RecordFileType == type.ID))
                    {
                        var time = temp.ExpirationTime == null
                            ? "无过期时间"
                            : temp.ExpirationTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                        html += "<div class=\"layui-row\" style=\"text-align: center;\">" +
                                "<div class=\"layui-col-xs4\">" + temp.FileName + "</div>" +
                                "<div class=\"layui-col-xs4\">" + time + "</div>" +
                                "<div class=\"layui-col-xs4\">x" + temp.Amount + "</div>" +
                                "</div>";
                    }

                    html += "</div></div></div>";
                }

                html += "</div>";
            }

            ViewBag.html = html;
            return View("RecordReturn");
        }

        public ActionResult ApplyBorrowAgree(string borrowId)
        {
            try
            {
                var applyBorrow = ApplyBorrowService.GetByWhere(" where BorrowID=@BorrowID", new {BorrowID = borrowId})
                    .First();
                applyBorrow.ApplyState = 2;
                var result = ApplyBorrowService.UpdateModel(applyBorrow);
                var editResult = ApplyBorrowService.ApplyBorrowAgree(borrowId);

                if (result && editResult)
                {
                    var applyRecordList = ApplyBorrowFileListService
                        .GetByWhere(" where BorrowID=@BorrowID", new {BorrowID = borrowId}).ToList();

                    foreach (var record in applyRecordList)
                    {
                        var operate = new OperateLog()
                        {
                            OperateTime = DateTime.Now,
                            OperateType = "审批借阅",
                            OperatePeople = Operator.RealName,
                            RecordId = record.RecordID
                        };
                        operate.OperateInfo += $"档案编号:{record.RecordID} 申请借阅审核通过";
                        OperateLogService.CreateModel(operate);
                    }
                    
                    return Json(SuccessTip("申请借阅审核成功!"));
                }
                else
                {
                    return Json(ErrorTip("申请借阅审核失败!"));
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.WriteFatal(ex);
                return Json(ErrorTip("获取不到申请借阅记录,出现数据一致性错误!"));
            }
        }

        public ActionResult BorrowList(int? id)
        {
            base.Index(id);
            return View();
        }

        public ActionResult GetBorrowList(ApplyBorrowTable filter, PageInfo pageInfo)
        {
            filter.ApplyUser = Operator.UserName;
            var result = ApplyBorrowService.GetListByFilter(filter, pageInfo);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult ReApplyBorrow(string borrowId)
        {
            try
            {
                var result = ApplyBorrowService.GetByWhere(" where BorrowID=@BorrowID", new { BorrowID = borrowId }).First();
                var borrowRecordList = ApplyBorrowFileListService.GetByWhere(" where BorrowID=@BorrowID", new { BorrowID = borrowId }).ToList();
                if(result.ApplyState == 3)
                {
                    result.ApplyState = 1;
                    var sign = ApplyBorrowService.UpdateModel(result);

                    if(sign)
                    {
                        foreach(var item in borrowRecordList)
                        {
                            var operate = new OperateLog()
                            {
                                OperatePeople = Operator.RealName,
                                OperateTime = DateTime.Now,
                                OperateType = "重新申请借阅",
                                RecordId = item.RecordID,
                                OperateInfo = $"重新申请借阅 借阅编号:{result.BorrowID},档案编号:{item.RecordID}"
                            };
                            OperateLogService.CreateModel(operate);
                        }
                        return Json(SuccessTip("重新申请借阅成功!"));
                    }
                    else
                    {
                        return Json(ErrorTip("重新申请借阅失败!"));
                    }
                }
                else
                {
                    return Json(ErrorTip("当前申请状态不允许重新申请!"));
                }
            }
            catch(InvalidOperationException ex)
            {
                Log.WriteFatal(ex);
                return Json(ErrorTip("重新申请借阅失败,当前借阅编号不存在!"));
            }
        }

        public ActionResult ApplyList(int? id)
        {
            base.Index(id);
            return View();
        }

        public ActionResult RecordListCopy(ApplyCopyTable filter, PageInfo pageInfo)
        {
            filter.ApplyUser = Operator.UserName;
            var result = ApplyCopyService.GetListByFilter(filter, pageInfo);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult CopyFileListDetail(string borrowId)
        {
            var copyRecordList = ApplyCopyFileListService
                .GetByWhere(" where BorrowID=@BorrowID", new { BorrowID = borrowId }).Select(item => item.RecordID)
                .ToList();

            var html = "";
            foreach (var recordId in copyRecordList)
            {
                var record = recordService.GetRecord(recordId);
                var typeList = ContractFileType.GetListByRecordId(recordId);
                var fileList = recordService.GetRecordListByRecordId(recordId);
                var otherFileList = recordService.GetOtherFileListByRecordId(recordId);

                html += "<fieldset class='layui-elem-field layui-field-title' style='margin-top:20px;'>" +
                        "<legend>" + record.RecordUserName + "</legend>" +
                        "</fieldset><div style='padding: 20px; background-color: #F2F2F2;'>";
                foreach (var type in typeList)
                {
                    html += "<div class=\"layui-card\">" +
                            "<div class=\"layui-card-header\">" + type.RecordTypeName + " " + type.HoldingCell + "</div>" +
                            "<div class=\"layui-card-body\">" +
                            "<div class=\"layui-fluid\">";

                    foreach (var temp in fileList.Where(item => item.RecordType == type.ID))
                    {
                        var time = temp.ExpirationTime == null
                            ? "无过期时间"
                            : temp.ExpirationTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                        html += "<div class=\"layui-row\" style=\"text-align: center;\">" +
                                "<div class=\"layui-col-xs4\">" + temp.RecordFileName + "</div>" +
                                "<div class=\"layui-col-xs4\">" + time + "</div>" +
                                "<div class=\"layui-col-xs4\">x" + temp.Amount + "</div>" +
                                "</div>";
                    }

                    foreach (var temp in otherFileList.Where(item => item.RecordFileType == type.ID))
                    {
                        var time = temp.ExpirationTime == null
                            ? "无过期时间"
                            : temp.ExpirationTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                        html += "<div class=\"layui-row\" style=\"text-align: center;\">" +
                                "<div class=\"layui-col-xs4\">" + temp.FileName + "</div>" +
                                "<div class=\"layui-col-xs4\">" + time + "</div>" +
                                "<div class=\"layui-col-xs4\">x" + temp.Amount + "</div>" +
                                "</div>";
                    }

                    html += "</div></div></div>";
                }

                html += "</div>";
            }

            ViewBag.html = html;
            return View("RecordReturn");
        }

        public ActionResult ApplyCopyAgree(string borrowId)
        {
            try
            {
                var applyCopy = ApplyCopyService.GetByWhere(" where BorrowID=@BorrowID", new { BorrowID = borrowId })
                    .First();
                applyCopy.ApplyState = 2;
                var result = ApplyCopyService.UpdateModel(applyCopy);

                if (result)
                {
                    var applyRecordList = ApplyCopyFileListService
                        .GetByWhere(" where BorrowID=@BorrowID", new { BorrowID = borrowId }).ToList();

                    foreach (var record in applyRecordList)
                    {
                        var operate = new OperateLog()
                        {
                            OperateTime = DateTime.Now,
                            OperateType = "审批调阅",
                            OperatePeople = Operator.RealName,
                            RecordId = record.RecordID
                        };
                        operate.OperateInfo += $"档案编号:{record.RecordID} 申请调阅审核通过";
                        OperateLogService.CreateModel(operate);
                    }

                    return Json(SuccessTip("申请调阅审核成功!"));
                }
                else
                {
                    return Json(ErrorTip("申请调阅审核失败!"));
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.WriteFatal(ex);
                return Json(ErrorTip("获取不到申请调阅记录,出现数据一致性错误!"));
            }
        }

        public ActionResult ApplyCopy(string recordIdStr)
        {
            var recordIdList = recordIdStr.Split(',').ToList();
            var failBorrowList = new List<string>();

            for (var i = recordIdList.Count - 1; i >= 0; i--)
            {
                var record = recordService.GetRecord(recordIdList[i]);
                if (record.RecordStatus != 2)
                {
                    failBorrowList.Add(recordIdList[i]);
                    recordIdList.RemoveAt(i);
                }
                else
                {
                    if (record.RecordManager != Operator.UserName)
                    {
                        failBorrowList.Add(recordIdList[i]);
                        recordIdList.RemoveAt(i);
                    }
                }
            }

            var timeNow = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var apply = new ApplyCopyTable()
            {
                BorrowID = timeNow,
                ApplyTime = DateTime.Now,
                ApplyUser = Operator.UserName,
                ApplyState = 1
            };

            var message = new StringBuilder();
            if (failBorrowList.Any())
            {
                message.Append($"  {string.Join(",", recordIdList)}申请调阅失败,失败原因:档案不在库或者不归属您所在的支行!");
            }

            if (recordIdList.Any())
            {
                message.Append($"{string.Join(",", recordIdList)}申请调阅成功");

                var result = ApplyCopyService.CopyRecord(timeNow, apply, recordIdList);
                if (result)
                {
                    foreach (var item in recordIdList)
                    {
                        var operate = new OperateLog()
                        {
                            RecordId = item,
                            OperatePeople = Operator.RealName,
                            OperateType = "申请调阅",
                            OperateTime = DateTime.Now,
                            OperateInfo = $"申请调阅 调阅号:{timeNow},档案编号:{item}"
                        };
                        OperateLogService.CreateModel(operate);
                    }
                    return Json(SuccessTip($"{message.ToString()},请等待审核!"));
                }
                else
                {
                    return Json(ErrorTip("申请调阅失败,请重试!"));
                }
            }
            else
            {
                return Json(ErrorTip("申请调阅失败!"));
            }

        }

        public ActionResult RefuseHandOver(string recordId, string refuseReason)
        {
            var record = recordService.GetRecord(recordId);

            var user = UserService.GetUserByUserName(record.RecordManager);

            //发送短信通知
            //待完成
            return Json(SuccessTip());
        }
    }
}