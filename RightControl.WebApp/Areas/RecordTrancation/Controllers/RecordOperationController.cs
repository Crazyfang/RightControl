using Newtonsoft.Json;
using RightControl.Common;
using RightControl.IService.RecordTrancation;
using RightControl.Model.RecordTrancation;
using RightControl.WebApp.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using RightControl.IService.Permissions;
using RightControl.Model;
using RightControl.IService;
using Newtonsoft.Json.Linq;

namespace RightControl.WebApp.Areas.RecordTrancation.Controllers
{
    public class RecordOperationController : BaseController
    {
        public IRoleService roleService { get; set; }

        public IDepartmentService departmentService { get; set; }
        public IFileListService FileListService { get; set; }
        public IRecordFileTypeService RecordFileTypeService { get; set; }
        public ISelectTypeService SelectTypeService { get; set; }
        public IRecordService RecordService { get; set; }
        public IApplyBorrowService ApplyBorrowService { get; set; }
        public IDepartmentService DepartmentService { get; set; }
        public IOtherFileListService OtherFileListService { get; set; }
        public IRecordListService RecordListService { get; set; }
        public IContractFileTypeService ContractFileTypeService { get; set; }
        public IOperateLogService OperateLogService { get; set; }
        public IRecordNeedCreateService RecordNeedCreateService { get; set; }
        public ILoanManagerService LoadManagerService { get; set; }
        public IInsRecordContractService InsRecordContractService { get; set; }
        public IChangedRecordManagerService ChangedRecordManagerService { get; set; }
        public IExpiredFileVerifyService ExpiredFileVerifyService { get; set; }
        public IUserService UserService { get; set; }

        // GET: RecordTrancation/RecordOperation
        [HttpGet]
        public ActionResult RecordAdd(int? id)
        {
            ViewBag.Department = Operator.DepartmentCode;
            return View();
        }

        /// <summary>
        /// 档案增添提交
        /// </summary>
        /// <param name="record">档案信息</param>
        /// <returns>增添是否成功反馈</returns>
        [HttpPost]
        public ActionResult RecordAdd(Record record)
        {
            record.CreateOn = DateTime.Now;
            record.RecordStatus = 1;
            //DateTime的最小值为0000-01-01，跟Sql Server的最小值冲突，故使用SqlDateTime的最小值代替
            //尝试使用可空元素代替当前字段，达到插入Null值的目的
            //record.HandOverTime = SqlDateTime.MinValue.ToDate();
            //获取生成的档案编号
            var recordId = Common.Common.CreateRecordId("AA", record.RecordManagerDepartment, RecordService.GetNum());
            //获取Post表单中勾选中的清单条目
            var itemList = Request.Form.AllKeys.Where(x => x.Contains("_Name"));
            //临时变量
            var otherFileList = new List<OtherFileList>();
            var fileList = new List<RecordList>();

            record.RecordID = recordId;

            //生成操作信息
            var operateLog = new OperateLog()
            {
                RecordId = record.RecordID,
                OperateType = "新增",
                OperatePeople = Operator.RealName,
                OperateTime = DateTime.Now
            };

            var recordFileTypeDic = new Dictionary<string, int>();

            foreach (var key in itemList)
            {
                //key值形式为AA_File_1_Name,AA代表条目所属清单类别,File代表是已维护类别中的清单（Other代表添加的临时性的其他清单）,1代表ID,Name代表清单名称
                var keyList = key.Split('_');
                var recordFileType = 0;
                if (!recordFileTypeDic.ContainsKey(keyList[0]))
                {
                    var model = new ContractFileType()
                    {
                        RecordID = recordId,
                        RecordTypeID = RecordFileTypeService.GetNumByStr(keyList[0].Substring(0, 2)),
                        HoldingCell = Request[keyList[0] + "_HoldingCell"],
                        OriginalRecordType = keyList[0]
                    };
                    recordFileType = ContractFileTypeService.Create(model);

                    recordFileTypeDic.Add(keyList[0], recordFileType);
                }
                else
                {
                    recordFileType = recordFileTypeDic[keyList[0]];
                }

                //临时性清单条目处理
                if (keyList[1] == "Other")
                {
                    if (!string.IsNullOrEmpty(Request[key]))
                    {
                        var otherFile = new OtherFileList()
                        {
                            RecordID = recordId,
                            RecordFileType = recordFileType,
                            FileName = Request[key],
                            Amount = Request[string.Join("_", keyList.Take(3).ToList()) + "_Amount"].ToInt(),
                            ExpirationTime = Request[string.Join("_", keyList.Take(3).ToList()) + "_Date"]
                                .ToDateOrNull()
                        };
                        operateLog.OperateInfo += $"{RecordFileTypeService.GetByWhere(" where RecordFileTypeString=@RecordFileTypeString", new { RecordFileTypeString = keyList[0].Substring(0, 2) }).First().RecordTypeName} ";
                        if(!string.IsNullOrEmpty(Request[keyList[0] + "_HoldingCell"]))
                        {
                            operateLog.OperateInfo += $"-{Request[keyList[0] + "_HoldingCell"]}";
                        }
                        operateLog.OperateInfo += $" 用户自定义文件:{otherFile.FileName} 数量:{otherFile.Amount} 过期时间:{otherFile.ExpirationTime} <br>";
                        otherFileList.Add(otherFile);
                    }
                }
                //已维护类别清单条目处理
                else
                {
                    if (Request[key] == "on")
                    {
                        var file = new RecordList()
                        {
                            RecordId = recordId,
                            Amount = Request[string.Join("_", keyList.Take(3).ToList()) + "_Amount"].ToInt(),
                            FileID = keyList[2].ToInt(),
                            ExpirationTime = Request[string.Join("_", keyList.Take(3).ToList()) + "_Date"]
                                .ToDateOrNull(),
                            RecordType = recordFileType
                        };
                        var fileName = FileListService.ReadModel(file.FileID).FileName;
                        operateLog.OperateInfo += $"{RecordFileTypeService.GetByWhere(" where RecordFileTypeString=@RecordFileTypeString", new { RecordFileTypeString = keyList[0].Substring(0, 2) }).First().RecordTypeName} ";
                        if (!string.IsNullOrEmpty(Request[keyList[0] + "_HoldingCell"]))
                        {
                            operateLog.OperateInfo += $"-{Request[keyList[0] + "_HoldingCell"]}";
                        }
                        operateLog.OperateInfo += $"预设文件:{fileName} 数量:{file.Amount} 过期时间:{file.ExpirationTime} <br>";
                        fileList.Add(file);
                    }
                }
            }

            //档案插入并返回是否成功提示
            var result = RecordService.InsertRecord(record, fileList, otherFileList);
            if (result)
            {
                Log.WriteInfo($"开始增加档案,操作用户:{Operator.RealName},档案编号:{record.RecordID},档案用户姓名:{record.RecordUserName},档案用户客户号:{record.RecordUserCode}");
                OperateLogService.CreateModel(operateLog);
                return Json(SuccessTip());
            }
            else
            {
                return Json(ErrorTip());
            }
        }

        /// <summary>
        /// 获取档案类别所属的清单列表
        /// </summary>
        /// <param name="recordType">档案类别</param>
        /// <returns>清单列表</returns>
        [HttpPost]
        public ActionResult GetFileList(int recordType)
        {
            //获取档案类别所拥有的文件类别编号
            var recordTypeListSelect = SelectTypeService
                .GetByWhere(" where SelectTypeNum=@SelectTypeNum", new {SelectTypeNum = recordType})
                .Select(item => item.FileType).ToList();

            //根据文件类别编号获取对应的文件类别
            var recordTypeList = RecordFileTypeService.GetAll(orderby: "order by ID")
                .Where(item => recordTypeListSelect.Any(i => i == item.ID));

            //获取档案文件集合
            var fileList = FileListService.GetAll(orderby: "order by FileID");
            //返回Json字符串
            var returnStringHeader = "";

            foreach (var str in recordTypeList)
            {
                var sign = true;

                if (str.RecordTypeName.Contains("要件类") || str.RecordTypeName.Contains("权证类"))
                {
                    returnStringHeader += "<div class='layui-colla-item'><h2 class='layui-colla-title'>" + str.RecordTypeName;
                    returnStringHeader += "<button class='layui-btn addNewItem' style='float:right' type='button'>新增</button></h2>";
                    returnStringHeader += "<div class='layui-colla-content' id='" + str.RecordFileTypeString + "'>";
                    returnStringHeader += "<div class='layui-form-item'><label class='layui-form-label'>合同号:</label>";
                    returnStringHeader +=
                        "<div class='layui-input-inline'><input type='text' name='" + str.RecordFileTypeString + "_HoldingCell' placeholder='合同号' autocomplete='off' class='layui-input'></div></div>";
                    sign = false;
                }

                if (str.RecordTypeName.Contains("担保"))
                {
                    returnStringHeader += "<div class='layui-colla-item'><h2 class='layui-colla-title'>" + str.RecordTypeName;
                    returnStringHeader += "<button class='layui-btn addNewItem' style='float:right' type='button'>新增</button></h2>";
                    returnStringHeader += "<div class='layui-colla-content' id='" + str.RecordFileTypeString + "'>";
                    returnStringHeader += "<div class='layui-form-item'><label class='layui-form-label'>名称:</label>";
                    returnStringHeader +=
                        "<div class='layui-input-inline'><input type='text' name='" + str.RecordFileTypeString + "_HoldingCell' placeholder='名称' autocomplete='off' class='layui-input'></div></div>";
                    sign = false;
                }

                if (sign)
                {
                    returnStringHeader += "<div class='layui-colla-item'>" +
                                          "<h2 class='layui-colla-title'>" + str.RecordTypeName + "</h2>";
                    returnStringHeader += "<div class='layui-colla-content' id='" + str.RecordFileTypeString + "'>";
                }

                //组装档案文件类别的档案文件
                foreach (var file in fileList.Where(item => item.RecordFileType == str.ID))
                {
                    returnStringHeader += "<div class='layui-form-item'>" +
                                          "<div class='layui-input-inline' style='width: auto'>" +
                                          "<input type='checkbox' name='" + str.RecordFileTypeString + "_File_" +
                                          file.FileID + "_Name' title='" + file.FileName + "'>" +
                                          "</div>" +
                                          "<div class='layui-input-inline'>" +
                                          "<input type='text' name='" + str.RecordFileTypeString + "_File_" +
                                          file.FileID + "_Date' placeholder='到期日' class='layui-input datetime'>" +
                                          "</div>" +
                                          "<div class='layui-input-inline'>" +
                                          "<div class='layui-row'>" +
                                          "<div class='layui-col-md3' style='text-align: right'>" +
                                          "<button type='button' class='layui-btn layui-btn-primary num_action_sub'>-</button>" +
                                          "</div>" +
                                          "<div class='layui-col-md6'>" +
                                          "<input type='text' value='1' name='" + str.RecordFileTypeString + "_File_" +
                                          file.FileID + "_Amount' class='layui-input num' style='text-align: center'>" +
                                          "</div>" +
                                          "<div class='layui-col-md3'>" +
                                          "<button type='button' class='layui-btn layui-btn-primary num_action_add'>+</button>" +
                                          "</div>" +
                                          "</div>" +
                                          "</div>" +
                                          "</div>";
                }

                //组装档案文件类别下的其他文件Html部分
                returnStringHeader += "<div class='OtherFile'>" +
                                      "</div>" +
                                      "<button type='button' class='layui-btn add'>添加其他</button>" +
                                      "</div>" +
                                      "</div>";
            }

            return Json(new {status = true, content = returnStringHeader}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据传递过来的档案文件类别生成页面内容
        /// </summary>
        /// <param name="recordFileType">档案文件类型</param>
        /// <param name="newFileTypeName">新档案文件名称</param>
        /// <returns>页面内容</returns>
        public ActionResult GetExtendContent(string recordFileType, string newFileTypeName)
        {
            var recordFileTypeObj = RecordFileTypeService.GetByWhere(" where RecordFileTypeString=@RecordFileTypeString", new { RecordFileTypeString = recordFileType }).FirstOrDefault();

            var pageContent = "";
            pageContent += "<div class='layui-colla-item'>";
            pageContent += "<h2 class='layui-colla-title'>" + recordFileTypeObj.RecordTypeName + newFileTypeName.Substring(2) + "<button class='layui-btn delItem' style='float:right' type='button'>删除</button></h2>";
            pageContent += "<div class='layui-colla-content' id='" + newFileTypeName + "'>";

            var recordFileList = FileListService.GetByWhere(" where RecordFileType=@RecordFileType", new { RecordFileType = recordFileTypeObj.ID });

            if (recordFileTypeObj.RecordTypeName.Contains("权证类") || recordFileTypeObj.RecordTypeName.Contains("要件类"))
            {
                pageContent += "<div class='layui-form-item'><label class='layui-form-label'>合同号:</label>";
                pageContent += "<div class='layui-input-inline'><input type='text' name='" + newFileTypeName + "_HoldingCell' placeholder='合同号' autocomplete='off' class='layui-input'></div></div>";
            }

            if (recordFileTypeObj.RecordTypeName.Contains("担保"))
            {
                pageContent += "<div class='layui-form-item'><label class='layui-form-label'>名称:</label>";
                pageContent += "<div class='layui-input-inline'><input type='text' name='" + newFileTypeName + "_HoldingCell' placeholder='名称' autocomplete='off' class='layui-input'></div></div>";
            }

            foreach (var item in recordFileList)
            {
                pageContent += "<div class='layui-form-item'>" +
                                "<div class='layui-input-inline' style='width: auto'>" +
                                "<input type='checkbox' name='" + newFileTypeName + "_File_" +
                                item.FileID + "_Name' title='" + item.FileName + "'>" +
                                "</div>" +
                                "<div class='layui-input-inline'>" +
                                "<input type='text' name='" + newFileTypeName + "_File_" +
                                item.FileID + "_Date' placeholder='到期日' class='layui-input datetime'>" +
                                "</div>" +
                                "<div class='layui-input-inline'>" +
                                "<div class='layui-row'>" +
                                "<div class='layui-col-md3' style='text-align: right'>" +
                                "<button type='button' class='layui-btn layui-btn-primary num_action_sub'>-</button>" +
                                "</div>" +
                                "<div class='layui-col-md6'>" +
                                "<input type='text' value='1' name='" + newFileTypeName + "_File_" +
                                item.FileID + "_Amount' class='layui-input num' style='text-align: center'>" +
                                "</div>" +
                                "<div class='layui-col-md3'>" +
                                "<button type='button' class='layui-btn layui-btn-primary num_action_add'>+</button>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</div>";
            }

            pageContent += "<div class='OtherFile'>" +
                            "</div>" +
                            "<button type='button' class='layui-btn add'>添加其他</button>" +
                            "</div>" +
                            "</div>";

            return Content(pageContent);
        }

        [HttpGet]
        public ActionResult RecordEdit(string recordId)
        {
            var record = RecordService.GetByWhere(" where RecordID=@RecordID", new {RecordID = recordId}).ToList()[0];
            return View(record);
        }

        /// <summary>
        /// 档案编辑提交
        /// </summary>
        /// <param name="record">档案信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RecordEdit(Record record)
        {
            //根据档案编号获取数据库中原有档案信息，用于比较档案类别是否发生了变化
            var oldRecord = RecordService.GetByWhere(" where RecordID=@RecordID", new {RecordID = record.RecordID})
                .FirstOrDefault();

            record.RecordStatus = oldRecord.RecordStatus;

            //档案信息为空，可能档案被删除或者其他原因
            if (oldRecord.IsEmpty())
            {
                return Json(ErrorTip("编辑失败，档案记录丢失，可能已被删除!"));
            }
            //档案不为空
            else
            {
                //从Post表单中获取档案清单条目
                var itemList = Request.Form.AllKeys.Where(x => x.Contains("_Name"));
                var otherFileList = new List<OtherFileList>();
                var fileList = new List<RecordList>();
                var insertList = new List<string>();

                //生成操作信息
                var operateLog = new OperateLog()
                {
                    RecordId = record.RecordID,
                    OperateType = "编辑",
                    OperatePeople = Operator.RealName,
                    OperateTime = DateTime.Now
                };

                foreach (var key in itemList)
                {
                    //key的形式为AA_File_1_Name
                    var keyList = key.Split('_');

                    if (keyList[1] == "Other")
                    {
                        if (!string.IsNullOrEmpty(Request[key]))
                        {
                            var contractFileType = ContractFileTypeService.GetByWhere(
                                " where RecordID=@RecordID and OriginalRecordType=@OriginalRecordType",
                                new {RecordID = record.RecordID, OriginalRecordType = keyList[0]}).FirstOrDefault();
                            var recordFileType = RecordFileTypeService.GetByWhere(" where RecordFileTypeString=@RecordFileTypeString", new { RecordFileTypeString = keyList[0].Substring(0, 2) }).First();

                            var contractId = 0;
                            if (contractFileType == null)
                            {
                                var model = new ContractFileType()
                                {
                                    RecordID = record.RecordID,
                                    RecordTypeID = RecordFileTypeService.GetNumByStr(keyList[0].Substring(0, 2)),
                                    HoldingCell = Request[keyList[0] + "_HoldingCell"],
                                    OriginalRecordType = keyList[0]
                                };
                                operateLog.OperateInfo += $"新增 {recordFileType.RecordTypeName}";
                                if(!string.IsNullOrEmpty(Request[keyList[0] + "_HoldingCell"]))
                                {
                                    operateLog.OperateInfo += $"-{Request[keyList[0] + "_HoldingCell"]}";
                                }
                                contractId = ContractFileTypeService.Create(model);
                            }
                            else
                            {
                                if (contractFileType.HoldingCell != Request[keyList[0] + "_HoldingCell"])
                                {
                                    contractFileType.HoldingCell = Request[keyList[0] + "_HoldingCell"];
                                    operateLog.OperateInfo += $"修改 {recordFileType.RecordTypeName}-{contractFileType.HoldingCell} 为 {recordFileType.RecordTypeName}-{Request[keyList[0] + "_HoldingCell"]}<br>";
                                    ContractFileTypeService.UpdateModel(contractFileType);
                                }
                                
                            }

                            var otherFile = new OtherFileList()
                            {
                                //赋予主键
                                ID = int.Parse(keyList[2]),
                                RecordID = record.RecordID,
                                //RecordFileTypeString = keyList[0],
                                //RecordFileType = RecordFileTypeService.GetNumByStr(keyList[0]),
                                RecordFileType = contractId == 0? contractFileType.ID : contractId,
                                FileName = Request[key],
                                Amount = Request[string.Join("_", keyList.Take(3).ToList()) + "_Amount"].ToInt(),
                                ExpirationTime = Request[string.Join("_", keyList.Take(3).ToList()) + "_Date"]
                                    .ToDateOrNull(),
                                OriginalRecordType = keyList[0]
                            };
                            otherFileList.Add(otherFile);
                        }
                    }
                    else
                    {
                        if (Request[key] == "on")
                        {
                            var contractFileType = ContractFileTypeService.GetByWhere(
                                " where RecordID=@RecordID and OriginalRecordType=@OriginalRecordType",
                                new { RecordID = record.RecordID, OriginalRecordType = keyList[0] }).FirstOrDefault();
                            var recordFileType = RecordFileTypeService.GetByWhere(" where RecordFileTypeString=@RecordFileTypeString", new { RecordFileTypeString = keyList[0].Substring(0, 2) }).First();

                            var contractId = 0;
                            if (contractFileType == null)
                            {
                                var model = new ContractFileType()
                                {
                                    RecordID = record.RecordID,
                                    RecordTypeID = RecordFileTypeService.GetNumByStr(keyList[0].Substring(0, 2)),
                                    HoldingCell = Request[keyList[0] + "_HoldingCell"],
                                    OriginalRecordType = keyList[0]
                                };
                                operateLog.OperateInfo += $"新增 {recordFileType.RecordTypeName}";
                                if (!string.IsNullOrEmpty(Request[keyList[0] + "_HoldingCell"]))
                                {
                                    operateLog.OperateInfo += $"-{Request[keyList[0] + "_HoldingCell"]}";
                                }
                                contractId = ContractFileTypeService.Create(model);
                            }
                            else
                            {
                                if (contractFileType.HoldingCell != Request[keyList[0] + "_HoldingCell"])
                                {
                                    contractFileType.HoldingCell = Request[keyList[0] + "_HoldingCell"];
                                    operateLog.OperateInfo += $"修改 {recordFileType.RecordTypeName}-{contractFileType.HoldingCell} 为 {recordFileType.RecordTypeName}-{Request[keyList[0] + "_HoldingCell"]}<br>";
                                    ContractFileTypeService.UpdateModel(contractFileType);
                                }
                            }

                            var file = new RecordList()
                            {
                                RecordId = record.RecordID,
                                Amount = Request[string.Join("_", keyList.Take(3).ToList()) + "_Amount"].ToInt(),
                                FileID = keyList[2].ToInt(),
                                ExpirationTime = Request[string.Join("_", keyList.Take(3).ToList()) + "_Date"]
                                    .ToDateOrNull(),
                                OriginalRecordType = keyList[0],
                                RecordType = contractId == 0 ? contractFileType.ID : contractId
                            };
                            fileList.Add(file);
                        }
                    }
                }

                //档案类型未发生改变
                if (oldRecord.RecordType == record.RecordType)
                {
                    var fileIdList = string.IsNullOrEmpty(Request["fileIdList"])
                        ? new List<string>()
                        : Request["fileIdList"].Split(',').ToList();
                    var otherFileIdList = string.IsNullOrEmpty(Request["otherFileIdList"])
                        ? new List<string>()
                        : Request["otherFileIdList"].Split(',').ToList();
                    
                    if (!RecordService.EditRecordNotChangeType(fileIdList, otherFileIdList, otherFileList, fileList,
                        record, operateLog))
                    {
                        return Json(ErrorTip("修改失败"));
                    }
                }
                //档案类型发生改变
                else
                {
                    if (!RecordService.EditRecordChangeType(otherFileList, fileList, record, operateLog))
                    {
                        return Json(ErrorTip("修改失败"));
                    }
                }
            }
            Log.WriteInfo($"修改档案 操作用户:{Operator.RealName},档案编号:{record.RecordID},档案用户姓名:{record.RecordUserName},档案用户客户号:{record.RecordUserCode}");
            return Json(SuccessTip("修改成功"));
        }

        /// <summary>
        /// 获取档案拥有的清单条目
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns></returns>
        public ActionResult GetTheItem(string recordId)
        {
            //获取档案所有的已维护清单条目
            var recordFileList = RecordService.GetRecordListByRecordId(recordId);
            //获取档案增添的其他清单条目
            var otherFileList = RecordService.GetOtherFileListByRecordId(recordId);

            var dic = new Dictionary<string, int>();

            var otherFileIdList = new List<string>();
            var fileIdList = new List<string>();

            //档案已维护清单条目标记（1），1是其主键
            foreach (var item in recordFileList)
            {
                if (item.OriginalRecordType.Length > 2)
                {
                    var counter = item.OriginalRecordType.Substring(2).ToInt();
                    if (dic.ContainsKey(item.OriginalRecordType.Substring(0, 2)))
                    {
                        if (dic[item.OriginalRecordType.Substring(0, 2)] < counter)
                        {
                            dic[item.OriginalRecordType.Substring(0, 2)] = counter;
                        }
                    }
                    else
                    {
                        dic.Add(item.OriginalRecordType.Substring(0, 2), counter);
                    }
                }

                fileIdList.Add(item.OriginalRecordType + "_" + item.FileID + "_" + item.RecordType);
            }

            //档案其他清单条目标记（AA(1)_1），AA(1)是其他清单条目所属的类别，1是其主键。便于编辑处理
            foreach (var item in otherFileList)
            {
                if (item.OriginalRecordType.Length > 2)
                {
                    var counter = item.OriginalRecordType.Substring(2).ToInt();
                    if (dic.ContainsKey(item.OriginalRecordType.Substring(0, 2)))
                    {
                        if (dic[item.OriginalRecordType.Substring(0, 2)] < counter)
                        {
                            dic[item.OriginalRecordType.Substring(0, 2)] = counter;
                        }
                    }
                    else
                    {
                        dic.Add(item.OriginalRecordType.Substring(0, 2), counter);
                    }
                }
                otherFileIdList.Add(item.OriginalRecordType + "_" + item.ID);
            }

            return Content(JsonConvert.SerializeObject(
                new
                {
                    recordFileList = recordFileList, OtherFileList = otherFileList, otherFileIdList = otherFileIdList,
                    fileIdList = fileIdList, clickDic = dic
                }), "application/json");

            //return Json(new { recordFileList = recordFileList, OtherFileList = otherFileList, otherFileIdList = otherFileIdList, fileIdList = fileIdList },
            //    JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecordIndex(int? id)
        {
            base.Index(id);
            return View();
        }

        public ActionResult RecordLists(Record filter, PageInfo pageInfo)
        {
            if(!string.IsNullOrEmpty(filter.RecordManagerDepartment))
            {
                var department = departmentService.ReadModel(Int32.Parse(filter.RecordManagerDepartment));
                filter.RecordManagerDepartment = department.DepartmentCode;
            }
            var result = RecordService.TestGetRecordPage(filter, pageInfo);
            //var result = RecordService.GetListByFilter(filter, pageInfo);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult RecordListLimited(Record filter, PageInfo pageInfo)
        {
            var role = roleService.ReadModel(Operator.RoleId);
            if (role.RoleName.Contains("客户经理"))
            {
                filter.RecordManager = Operator.UserName;
                filter.RecordManagerDepartment = Operator.DepartmentCode;
            }
            else if (role.RoleName.Contains("支行"))
            {
                filter.RecordManagerDepartment = Operator.DepartmentCode;
            }
            var result = RecordService.TestGetRecordPage(filter, pageInfo);
            
            //var result = RecordService.GetListByFilter(filter, pageInfo);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult RecordDetail(string recordId)
        {
            //根据档案编号获取档案信息(包括档案归属部门名称，档案归属客户经理姓名等)
            var record = RecordService.GetRecord(recordId);
            //var record = recordService.GetByWhere(" where RecordID=@RecordID", new {RecordID = recordId})
            //    .FirstOrDefault();

            return View(record);
        }

        /// <summary>
        /// 档案详情页档案文件显示
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns></returns>
        public ActionResult RecordFileList(string recordId)
        {
            var typeList = RecordService.GetTheTypeList(recordId);
            var fileList = RecordService.GetRecordListByRecordId(recordId);
            var otherFileList = RecordService.GetOtherFileListByRecordId(recordId);
            var html = "<div style='padding: 20px; background-color: #F2F2F2;'><div class='layui-row layui-col-space15'><div class='layui-col-md12'>";

            foreach (var type in typeList)
            {
                html += "<div class=\"layui-card\">" +
                        "<div class=\"layui-card-header\">" + type.RecordTypeName + "-" + type.HoldingCell +"</div>" +
                        "<div class=\"layui-card-body\">" +
                        "<div class=\"layui-fluid\">";

                foreach (var temp in fileList.Where(item=>item.RecordType==type.ID))
                {
                    var time = temp.ExpirationTime == null
                        ? "无过期时间"
                        : temp.ExpirationTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                    html += "<div class=\"layui-row\" style=\"text-align: center;\">" +
                            "<div class=\"layui-col-xs4\">"+ temp.RecordFileName +"</div>" +
                            "<div class=\"layui-col-xs4\">"+ time +"</div>" +
                            "<div class=\"layui-col-xs4\">x"+ temp.Amount +"</div>" +
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

            html += "</div></div></div>";

            return Content(html);
        }

        /// <summary>
        /// 档案删除
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns>删除成功与否</returns>
        public ActionResult RecordDelete(string recordId)
        {
            var record = RecordService.GetRecord(recordId);
            if (record.RecordStatus == 3)
            {
                return Json(ErrorTip("只能删除待移交和在库状态的档案！"));
            }

            var result = RecordService.DeleteRecord(recordId) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            return Json(result);
        }

        public ActionResult BorrowRecord(string recordIdString)
        {
            var recordIdList = recordIdString.Split(',').ToList();

            var timeNow = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var apply = new ApplyBorrowTable()
            {
                BorrowID = timeNow,
                ApplyTime = DateTime.Now,
                ApplyUser = Operator.DepartmentCode
            };
            var result = ApplyBorrowService.ApplyBorrow(timeNow, apply, recordIdList, Operator.RealName);

            if(result)
            {
                foreach(var recordId in recordIdList)
                {
                    var record = RecordService.GetByWhere(" where RecordID=@RecordID", new { RecordID = recordId }).First();
                    var operateLog = new OperateLog()
                    {
                        OperateType = "申请借阅",
                        RecordId = recordId,
                        OperatePeople = Operator.RealName,
                        OperateInfo = $"借阅档案 档案号:{recordId} 档案用户姓名:{record.RecordUserName} 档案用户客户号:{record.RecordUserCode} ",
                        OperateTime = DateTime.Now
                    };
                    OperateLogService.CreateModel(operateLog);
                }

                return Json(SuccessTip("借阅成功!"));
            }
            else
            {
                return Json(ErrorTip("借阅失败!"));
            }
        }

        public ActionResult RecordList(string recordId)
        {
            ViewBag.recordId = recordId;
            return View();
        }

        public ActionResult GetRecordInforDif()
        {
            return Json(RecordService.GetRecordInfoDif(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecordIndexManager(int? id)
        {
            base.Index(id);
            return View();
        }

        public ActionResult CheckEditLimit(string recordId)
        {
            var record = RecordService.GetRecord(recordId);

            if (record.RecordManager == Operator.UserName && record.RecordStatus == 1)
            {
                return Json(SuccessTip());
            }
            else
            {
                return Json(ErrorTip());
            }
        }

        public ActionResult RecordTransfer()
        {
            return View();
        }

        public ActionResult RecordExpired(Record filter, PageInfo pageInfo)
        {
            //var record = RecordService.GetPageMulTable(filter, pageInfo);
            var record = RecordService.GetExpiredFileRecord(filter, pageInfo);

            return Content(JsonConvert.SerializeObject(record), "application/json");
        }
        
        public ActionResult ExpiredFileRecord()
        {
            return View();
        }

        public ActionResult RecordExpiredByUser(Record filter, PageInfo pageInfo)
        {
            var recordList = RecordService.GetExpiredFileRecord(filter, pageInfo, Operator.UserName);

            return Content(JsonConvert.SerializeObject(recordList), "application/json");
        }

        public ActionResult ExpiredFile(string recordId)
        {
            ViewBag.recordId = recordId;

            return View();
        }

        public ActionResult ExpiredFileList(string recordId)
        {
            var contractList = ContractFileTypeService.GetListByRecordId(recordId);
            var fileList = RecordListService.GetRecordListExpired(recordId);
            var otherFileList = OtherFileListService.GetExpiredFile(recordId);
            //var otherFileList = OtherFileListService.GetByWhere(
            //    " where RecordID=@RecordID and ExpirationTime < GETDATE()",
            //    new {RecordID = recordId}, null, " order by ID");

            var html = new StringBuilder();

            contractList = contractList.Where(i => fileList.Select(a => a.RecordType).Contains(i.ID) 
                                                || otherFileList.Select(a => a.RecordFileType).Contains(i.ID)).ToList();

            foreach(var contract in contractList)
            {
                html.Append("<fieldset class='layui-elem-field'>" +
                            $"<legend>{contract.RecordTypeName}-{contract.HoldingCell}</legend>" +
                            "<div class='layui-field-box'>");

                foreach(var item in fileList.Where(i => i.RecordType == contract.ID))
                {
                    html.Append("<div class='layui-form-item'>");
                    html.Append("<div class='layui-input-inline'><label class='layui-form-lable'>" + item.File.FileName +
                                "</lable></div>");
                    html.Append(
                        "<div class='layui-input-inline'><input class='layui-input datetime' type='text' placeholder='选择过期时间' name='File_" +
                        item.ID + "'></div>");

                    html.Append("<div class='layui-input-inline'><label class='layui-form-label'>x" + item.Amount +
                                "</label></div>");
                    html.Append("<div class='layui-input-inline'><input type='checkbox' name='File_" + item.ID + "_remove' lay-skin='switch' lay-text='删除|保留'></div>");
                    html.Append("</div>");
                }

                foreach(var item in otherFileList.Where(i => i.RecordFileType == contract.ID))
                {
                    html.Append("<div class='layui-form-item'>");
                    html.Append("<div class='layui-input-inline'><label class='layui-form-lable'>" + item.FileName +
                                "</lable></div>");
                    html.Append(
                        "<div class='layui-input-inline'><input class='layui-input datetime' type='text' placeholder='选择过期时间' name='Other_" +
                        item.ID + "'></div>");

                    html.Append("<div class='layui-input-inline'><label class='layui-form-label'>x" + item.Amount +
                                "</label></div>");
                    html.Append("<div class='layui-input-inline'><input type='checkbox' name='Other_" + item.ID + "_remove' lay-skin='switch' lay-text='删除|保留'></div>");
                    html.Append("</div>");
                }

                html.Append("</div></fieldset>");
            }
            //foreach (var item in fileList)
            //{
            //    html.Append("<div class='layui-form-item'>");
            //    html.Append("<div class='layui-input-inline'><label class='layui-form-lable'>" + item.RecordFileName +
            //                "</lable></div>");
            //    html.Append(
            //        "<div class='layui-input-inline'><input class='layui-input datetime' type='text' placeholder='选择过期时间' name='File_" +
            //        item.ID + "'></div>");

            //    html.Append("<div class='layui-input-inline'><label class='layui-form-label'>x" + item.Amount +
            //                "</label></div>");
            //    html.Append("</div>");
            //}

            //foreach (var item in otherFileList)
            //{
            //    html.Append("<div class='layui-form-item'>");
            //    html.Append("<div class='layui-input-inline'><label class='layui-form-lable'>" + item.FileName +
            //                "</lable></div>");
            //    html.Append(
            //        "<div class='layui-input-inline'><input class='layui-input datetime' type='text' placeholder='选择过期时间' name='Other_" +
            //        item.ID + "'></div>");

            //    html.Append("<div class='layui-input-inline'><label class='layui-form-label'>x" + item.Amount +
            //                "</label></div>");
            //    html.Append("</div>");
            //}

            html.Append("<div class='layui-form-item' style='margin-top: 10px; text-align: center' id='buttonGroup'><div class='layui-input-block'>");
            html.Append("<button class='layui-btn' lay-submit lay-filter='formDemo' id='submit_btn'>立即提交</button></div></div>");

            return Content(html.ToString());
        }

        public ActionResult ChangeFileValidateTime()
        {
            var fileList = Request.Form.AllKeys.Where(x => x.Contains("File_") && !x.Contains("_remove"));
            var otherFileList = Request.Form.AllKeys.Where(x => x.Contains("Other_") && !x.Contains("_remove"));
            var expiredFileList = new List<ExpiredFileVerifyEntity>();

            var operateLog = new OperateLog()
            {
                OperatePeople = Operator.RealName,
                OperateType = "过期文件更新申请",
                OperateTime = DateTime.Now
            };
            try
            {
                foreach (var item in fileList)
                {
                    var id = int.Parse(item.Split('_')[1]);
                    var expiredFile = new ExpiredFileVerifyEntity()
                    {
                        RecordFileId = id,
                        RecordDelSign = string.IsNullOrEmpty(Request[item + "_remove"]) ? false : true,
                        RecordFileDate = Request[item].ToDateOrNull(),
                        OperateTime = DateTime.Now
                    };
                    var recordFile = RecordListService.ReadModel(id);
                    var contract = ContractFileTypeService.ReadModel(recordFile.RecordType);
                    var recordFileType = RecordFileTypeService.ReadModel(contract.RecordTypeID);
                    if (string.IsNullOrEmpty(operateLog.RecordId))
                    {
                        operateLog.RecordId = recordFile.RecordId;
                    }

                    operateLog.OperateInfo += $"更新 {recordFileType.RecordTypeName}-{contract.HoldingCell} 预设文件{recordFile.RecordFileName} ";
                    if (expiredFile.RecordDelSign)
                    {
                        operateLog.OperateInfo += $"删除 <br>";
                    }
                    else
                    {
                        operateLog.OperateInfo += $"<br> 原过期时间 {recordFile.ExpirationTime} 申请更新过期时间 {expiredFile.RecordFileDate} <br>";
                    }
                    expiredFileList.Add(expiredFile);
                    //var recordList = RecordListService.ReadModel(id);
                    //recordList.ExpirationTime = Request[item].ToDateOrNull();
                    //RecordListService.UpdateModel(recordList);
                }

                foreach (var item in otherFileList)
                {
                    var id = int.Parse(item.Split('_')[1]);
                    var otherFile = OtherFileListService.ReadModel(id);
                    var contract = ContractFileTypeService.ReadModel(otherFile.RecordFileType);
                    var recordFileType = RecordFileTypeService.ReadModel(contract.RecordTypeID);
                    if (string.IsNullOrEmpty(operateLog.RecordId))
                    {
                        operateLog.RecordId = otherFile.RecordID;
                    }
                    var expiredFile = new ExpiredFileVerifyEntity()
                    {
                        OtherFileId = id,
                        OtherDelSign = string.IsNullOrEmpty(Request[item + "_remove"]) ? false : true,
                        OtherFileDate = Request[item].ToDateOrNull(),
                        OperateTime = DateTime.Now
                    };
                    operateLog.OperateInfo += $"更新 {recordFileType.RecordTypeName}-{contract.HoldingCell} 用户自定义文件{otherFile.FileName} ";
                    if (expiredFile.RecordDelSign)
                    {
                        operateLog.OperateInfo += $"删除 <br>";
                    }
                    else
                    {
                        operateLog.OperateInfo += $"<br> 原过期时间 {otherFile.ExpirationTime} 申请更新过期时间 {expiredFile.RecordFileDate} <br>";
                    }
                    expiredFileList.Add(expiredFile);

                    //var otherFile = OtherFileListService.ReadModel(id);
                    //otherFile.ExpirationTime = Request[item].ToDateOrNull();
                    //OtherFileListService.UpdateModel(otherFile);
                }

                OperateLogService.CreateModel(operateLog);
                var result = ExpiredFileVerifyService.InsertItems(expiredFileList) ? SuccessTip("已提交更新请求，等待管理员审核") : ErrorTip("更新失败,请重试!");
                return Json(result);
            }
            catch(Exception e)
            {
                return Json(ErrorTip());
            }
            
        }

        public ActionResult RecordBelongChange()
        {
            JArray recordChanged = JArray.Parse(Request["otherReciever"].ToString());
            //foreach(var item in recordChanged)
            //{
            //    Console.WriteLine("123");
            //}
            //var test = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TestModel>>(Request["otherReciever"].ToString());
            var result = RecordService.RecordBelongChange(recordChanged, Operator.RealName) ? SuccessTip() : ErrorTip();
            //var originalUser = Request["user_original"];
            //var nowUser = Request["user_now"];

            //var result = RecordService.ChangeRecordBelong(originalUser, nowUser) ? SuccessTip() : ErrorTip();
            //var result = SuccessTip();
            return Json(result);
        }

        public ActionResult RecordBelongList(string userCode)
        {
            var data = RecordService.RecordBelongList(userCode);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecordNeedCrateIndex(int? id)
        {
            base.Index(id);
            return View("RecordNeedCreate");
        }

        public ActionResult RecordNeedCreateList(RecordNeedCreate filter, PageInfo pageInfo)
        {
            var result = RecordNeedCreateService.GetListByFilter(filter, pageInfo);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult RecordCreate(string custINNO)
        {
            var record = new Record();
            try
            {
                var recordNeedCreate = RecordNeedCreateService
                    .GetByWhere(" where CustINNO=@CustINNO", new {CustINNO = custINNO }).First();
                record.RecordUserInCode = recordNeedCreate.CustINNO.Trim();
                record.RecordUserCode = recordNeedCreate.CustNO.Trim();
                record.RecordUserName = recordNeedCreate.Custname.Trim();
            }
            catch (InvalidOperationException e)
            {
                Log.WriteLog(e);
            }

            return View(record);
        }

        /// <summary>
        /// 获得存量有贷客户的客户经理的部门和客户经理的柜员号(存在多位客户经理管理一个档案，发出提示)
        /// </summary>
        /// <param name="CustInCode">客户内码</param>
        /// <returns>部门编号 客户经理列表</returns>
        public ActionResult GetDepAndManager(string custInCode)
        {
            var result = LoadManagerService.GetLoanManagersByCustInCode(custInCode);

            return Json(result);
        }

        public ActionResult ContractDelView(int? id)
        {
            base.Index(id);
            return View();
        }

        public ActionResult ContractDelList(InsRecordContract filter, PageInfo pageInfo)
        {
            pageInfo.field = "ID";
            pageInfo.order = "asc";
            var result = InsRecordContractService.GetListByFilter(filter, pageInfo);

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult DelContract(string contractNo, string custInNo)
        {
            try
            {
                var result = InsRecordContractService.GetByWhere(" where ContractNo=@ContracNo and CustINNO=@CustINNO",
                    new {ContractNo = contractNo, CustINNO = custInNo}).First();

                if (result.DeleteSign == 1)
                {
                    return Json(ErrorTip("该条合同信息已被移除,请勿重复操作，刷新后再尝试!"));
                }
                else
                {
                    result.DeleteSign = 1;
                    var record = RecordService.GetByWhere(" where RecordUserInCode=@RecordUserInCode",
                        new {RecordUserInCode = result.CustINNO}).FirstOrDefault();

                    if (!string.IsNullOrEmpty(record.RecordID))
                    {
                        return Json(ErrorTip("合同相关联的档案信息不存在,删除失败,请刷新后再尝试!"));
                    }
                    else
                    {
                        //删除相关合同的文件
                        var contractFileList = ContractFileTypeService
                            .GetByWhere(" where RecordID=@RecordID and HoldingCell=@HoldingCell",
                                new {RecordID = record.RecordID, HoldingCell = contractNo}).ToList();

                        foreach (var item in contractFileList)
                        {
                            RecordListService.DeleteByWhere($" where RecordID='{item.RecordID}' and RecordType={item.RecordTypeID}");
                            OtherFileListService.DeleteByWhere(
                                $" where RecordID='{item.RecordID}' and RecordFileType={item.RecordTypeID}");
                        }

                        //生成操作日志
                        var operate = new OperateLog()
                        {
                            OperatePeople = Operator.RealName,
                            OperateType = "删除过期合同",
                            OperateTime = DateTime.Now,
                            RecordId = record.RecordID
                        };

                        var sign = InsRecordContractService.UpdateModel(result);
                        if(sign)
                        {
                            operate.OperateInfo =
                                $"删除过期合同 档案号:{operate.RecordId} 合同号:{result.ContractNo} 客户内码:{result.CustINNO} <br>";
                            OperateLogService.CreateModel(operate);
                            return Json(SuccessTip("成功删除过期合同信息!"));
                        }
                        else
                        {
                            return Json(ErrorTip("删除过期合同信息失败!"));
                        }
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Log.WriteLog(e);
                return Json(ErrorTip("寻找不到该条信息，请重试!"));
            }
        }

        public ActionResult ChangedRecordManagerIndex()
        {
            return View();
        }

        public ActionResult RecordTransferNew()
        {
            return View();
        }

        public ActionResult ChangedRecordManagerList(ChangedRecordManager filter, PageInfo pageInfo)
        {
            var result = ChangedRecordManagerService.GetListByFilter(filter, pageInfo);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult ChangeManagerView(string recordId)
        {
            var result = RecordService.GetByWhere(" where RecordID=@RecordID", new {RecordID = recordId}).First();
            return View(result);
        }

        public ActionResult ChangeRecordManger(Record record)
        {
            var result = RecordService.UpdateModel(record, "RecordManager");

            var operate = new OperateLog()
            {
                OperateTime = DateTime.Now,
                OperateType = "手动修改档案归属",
                OperatePeople = Operator.RealName,
                OperateInfo = $"档案归属修改 修改为客户经理柜员号:{record.RecordManager} <br>"
            };

            if (result)
            {
                OperateLogService.CreateModel(operate);
                return Json(SuccessTip("修改成功!"));
            }
            else
            {
                return Json(ErrorTip("修改失败!"));
            }
        }

        public ActionResult ExpiredFileCheckList()
        {
            return View();
        }

        public ActionResult ExpiredFileAccept(string recordId)
        {
            ViewBag.recordId = recordId;
            return View();
        }

        public ActionResult ExpiredFileListData(PageInfo pageInfo)
        {
            var result = RecordService.NeedVerifyExpiredFileList(pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetExpiredFileCompare(string recordId)
        {
            var contractList = ContractFileTypeService.GetListByRecordId(recordId);
            var dic = RecordService.GetExpiredFileCompare(recordId);

            var recordList = dic.recordList;
            var otherList = dic.otherList;
            var expiredVerifyList = dic.expiredVerifyList;

            if (recordList.Count > 0)
            {
                var item = recordList.Select(a => a.RecordType);
                contractList = contractList.Where(i => item.Contains(i.ID)).ToList();
            }

            if (otherList.Count > 0)
            {
                var item = otherList.Select(a => a.RecordFileType);
                contractList = contractList.Where(i => item.Contains(i.ID)).ToList();
            }

            var html = new StringBuilder();

            foreach (var contract in contractList)
            {
                html.Append("<div class='layui-col-xs6 layui-col-sm6 layui-col-md6'>"+
                            "<fieldset class='layui-elem-field'>" +
                            $"<legend>{contract.RecordTypeName}-{contract.HoldingCell}</legend>" +
                            "<div class='layui-field-box'>");

                foreach (var item in recordList.Where(i => i.RecordType == contract.ID))
                {
                    html.Append("<div class='layui-row'>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + item.File.FileName + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + item.ExpirationTime + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>x" + item.Amount + "</div>");
                    html.Append("</div>");
                }

                foreach (var item in otherList.Where(i => i.RecordFileType == contract.ID))
                {
                    html.Append("<div class='layui-row'>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + item.FileName + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + item.ExpirationTime + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>x" + item.Amount + "</div>");
                    html.Append("</div>");
                }

                html.Append("</div></fieldset></div>");
            }

            foreach (var contract in contractList)
            {
                html.Append("<div class='layui-col-xs6 layui-col-sm6 layui-col-md6'>" +
                            "<fieldset class='layui-elem-field'>" +
                            $"<legend>{contract.RecordTypeName}-{contract.HoldingCell}</legend>" +
                            "<div class='layui-field-box'>");

                foreach (var item in recordList.Where(i => i.RecordType == contract.ID))
                {
                    var expire = expiredVerifyList.Where(i => i.RecordFileId == item.ID).FirstOrDefault();
                    var data = expire.RecordDelSign ? "删除" : expire.RecordFileDate.ToString();
                    html.Append("<div class='layui-row'>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + item.File.FileName + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + data + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>x" + item.Amount + "</div>");
                    html.Append("</div>");
                }

                foreach (var item in otherList.Where(i => i.RecordFileType == contract.ID))
                {
                    var expire = expiredVerifyList.Where(i => i.OtherFileId == item.ID).FirstOrDefault();
                    var data = expire.OtherDelSign ? "删除" : expire.OtherFileDate.ToString();
                    html.Append("<div class='layui-row'>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + item.FileName + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>" + data + "</div>");
                    html.Append("<div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>x" + item.Amount + "</div>");
                    html.Append("</div>");
                }

                html.Append("</div></fieldset></div>");
            }

            return Content(html.ToString());
        }

        public ActionResult RefuseExpiredFileChange(string reason, string recordId)
        {
            var record = RecordService.GetRecord(recordId);
            var user = UserService.GetUserByUserName(record.RecordManager);
            //发送短信
            //待完成

            var sign = ExpiredFileVerifyService.RefuseExpiredFileChange(reason, recordId);
            var operateLog = new OperateLog()
            {
                RecordId = recordId,
                OperatePeople = Operator.RealName,
                OperateTime = DateTime.Now,
                OperateType = "过期文件更新申请拒绝",
                OperateInfo = $"过期文件更新申请审核未通过，拒绝理由:{reason}"
            };
            OperateLogService.CreateModel(operateLog);
            var result = sign ? SuccessTip() : ErrorTip();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AcceptExpiredFileChange(string recordId)
        {
            var sign = ExpiredFileVerifyService.AcceptExpiredFileChange(recordId);
            var operateLog = new OperateLog()
            {
                RecordId = recordId,
                OperatePeople = Operator.RealName,
                OperateTime = DateTime.Now,
                OperateType = "过期文件更新申请通过",
                OperateInfo = "过期文件更新申请审核通过，更新至文件"
            };
            OperateLogService.CreateModel(operateLog);
            var result = sign ? SuccessTip() : ErrorTip();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecordHistory(string recordId)
        {
            ViewBag.recordId = recordId;
            return View();
        }

        public ActionResult RecordHistoryData(string recordId)
        {
            var list = RecordService.GetRecordHistory(recordId);

            return Json(JsonConvert.SerializeObject(list), JsonRequestBehavior.AllowGet);
        }
    }
}