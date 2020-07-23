using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using RightControl.IService;
using RightControl.IService.InforSearch;
using RightControl.Model;
using RightControl.Model.InforSearch;
using RightControl.WebApp.Areas.Admin.Controllers;

namespace RightControl.WebApp.Areas.InforSearch.Controllers
{
    public class BlackListController : BaseController
    {
        public IBlackListService blackListService { get; set; }

        public IUserService userService { get; set; }

        public IInforSearchOperateService InforSearchOperateService { get; set; }

        [HttpGet]
        public JsonResult List(BlackList filter, PageInfo pageInfo)
        {
            if (!string.IsNullOrEmpty(filter.BankName))
            {
                var inforSearchObj = new InforSearchOperate()
                {
                    CreateOn = DateTime.Now,
                    OperaterId = Operator.UserId,
                    OperateMsg = "搜索    近似银行搜索词:"+filter.BankName
                };
                InforSearchOperateService.CreateModel(inforSearchObj);
            }
            var result = blackListService.GetListByFilter(filter, pageInfo);
            //Type t = result.GetType();
            //PropertyInfo p = t.GetProperty("count");
            //object v = p.GetValue(result, null);
            object v = result?.GetType().GetProperty("count")?.GetValue(result, null);
            if (v != null)
            {
                if ((long) v == 0)
                {
                    var inforSearchObj = new InforSearchOperate()
                    {
                        CreateOn = DateTime.Now,
                        OperaterId = Operator.UserId,
                        OperateMsg = $"搜索    近似银行搜索词:{filter.BankName} 无搜索结果，已提示您搜索的承兑行 {filter.BankName} 无搜索结果，请您根据实际情况自行把控风险！"
                    };
                    InforSearchOperateService.CreateModel(inforSearchObj);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Detail(int Id)
        {
            var obj = blackListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            obj.EditorName = userService.GetDetail(obj.EditorId).RealName;
            obj.SearchMan = Operator.RealName;
            obj.SearchBranch = Operator.DepartmentName;
            obj.SearchTime = DateTime.Now;

            if (!string.IsNullOrEmpty(obj.BankName))
            {
                var inforSearchObj = new InforSearchOperate()
                {
                    CreateOn = DateTime.Now,
                    OperaterId = Operator.UserId,
                    OperateMsg = $"查看    银行名为{obj.BankName},拉黑原因{obj.RefuseReason}"
                };
                InforSearchOperateService.CreateModel(inforSearchObj);
            }

            return View(obj);
        }

        [HttpPost]
        public ActionResult Add(BlackList obj)
        {
            obj.CreateOn = DateTime.Now;
            obj.EditorId = Operator.UserId;
            var result = blackListService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");

            if ((string)result.state == "success")
            {
                var msg = $"添加    银行名{obj.BankName},拉黑原因{obj.RefuseReason}";
                var inforSearchObj = new InforSearchOperate()
                {
                    OperaterId = Operator.UserId,
                    CreateOn = DateTime.Now,
                    OperateMsg = msg
                };

                InforSearchOperateService.CreateModel(inforSearchObj);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var obj = blackListService.GetByWhere(" where Id=@Id", new {Id = Id}).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(BlackList obj)
        {
            var oldModel = blackListService.GetByWhere(" where Id=@Id", new {Id = obj.Id}).ToList()[0];
            var result = blackListService.UpdateModel(obj) ? SuccessTip("修改成功!") : ErrorTip("修改失败!");
            if ((string)result.state == "success")
            {
                var msg = $"修改    原银行名为{oldModel.BankName},新银行名为{obj.BankName},原拒绝原因为{oldModel.RefuseReason},新拒绝原因为{obj.RefuseReason}";
                var inforSearchObj = new InforSearchOperate()
                {
                    OperaterId = Operator.UserId,
                    CreateOn = DateTime.Now,
                    OperateMsg = msg
                };

                InforSearchOperateService.CreateModel(inforSearchObj);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            var oldModel = blackListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            var result = blackListService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            if ((string)result.state == "success")
            {
                var msg = $"删除    银行名为{oldModel.BankName},拒绝原因为{oldModel.RefuseReason}的条目";
                var inforSearchObj = new InforSearchOperate()
                {
                    OperaterId = Operator.UserId,
                    CreateOn = DateTime.Now,
                    OperateMsg = msg
                };

                InforSearchOperateService.CreateModel(inforSearchObj);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTheOperateLog(InforSearchOperate filter, PageInfo pageInfo)
        {
            var result = InforSearchOperateService.GetPageMulTable(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OperateLog()
        {
            return View();
        }
    }
}