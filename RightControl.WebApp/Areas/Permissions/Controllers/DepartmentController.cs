using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RightControl.Common;
using RightControl.Common.Extend;
using RightControl.IService;
using RightControl.Model;
using System.Web.Mvc;
using RightControl.IRepository.InforSearch;
using RightControl.IService.Permissions;
using RightControl.Model.Permissions;
using RightControl.WebApp.Areas.Admin.Controllers;
using System.Text;
using System.Threading.Tasks;
using RightControl.IService.RecordSetting;

namespace RightControl.WebApp.Areas.Permissions.Controllers
{
    public class DepartmentController : BaseController
    {
        public IDepartmentService DepartmentService { get; set; }
        public ITestService TestService { get; set; }
        // GET: Permissions/Department
        public ActionResult GetTrees(string id)
        {
            return Json(DepartmentService.GetTrees(id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(string id)
        {
            DepartmentModel model = new DepartmentModel();
            if (id == "0")
            {
                ViewBag.ParentName = "";
            }
            else
            {
                var parentDepartment = DepartmentService.GetByWhere("where Id=@Id", new {Id = id}, "Id,DepartmentName")
                    .ToList()[0];
                ViewBag.ParentName = parentDepartment.DepartmentName;
                model.ParentId = parentDepartment.Id;
            }
            
            return View(model);
        }

        public ActionResult AddDepartment(DepartmentModel model)
        {
            model.CreateBy = Operator.UserId;
            model.CreateOn = DateTime.Now;
            var result = DepartmentService.CreateModel(model) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        public async Task<ActionResult> Get()
        {
            var result =  await TestService.GetAsync(1);
            return Json(result);
        }

        public ActionResult Edit(string id)
        {
            var model = DepartmentService.ReadModel(int.Parse(id));
            if (model.ParentId != 0)
            {
                ViewBag.ParentName =
                    DepartmentService.GetByWhere("where Id=@Id", new {Id = model.ParentId}, "Id,DepartmentName")
                        .ToList()[0]
                        .DepartmentName;
            }
            else
            {
                ViewBag.ParentName = "";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepartmentModel model)
        {
            var orignalmodel = DepartmentService.ReadModel(model.Id);
            model.UpdateBy = Operator.UserId;
            model.UpdateOn = DateTime.Now;
            model.CreateBy = orignalmodel.CreateBy;
            model.CreateOn = orignalmodel.CreateOn;

            var result = DepartmentService.UpdateModel(model) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        public ActionResult Delete(string id)
        {
            var result = DepartmentService.DeleteModel(int.Parse(id)) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        public ActionResult GetAllDepartment()
        {
            var lists = DepartmentService.GetAllDepartment();
            StringBuilder str = new StringBuilder();
            if (lists.Count > 0)
            {
                str.Append(Recursion(lists, 0));
                str = str.Remove(str.Length - 2, 2);
            }
            return Content(str.ToString());
        }

        public ActionResult GetSelectDepartment()
        {
            var departmentList = DepartmentService.GetAllDepartment();
            var sbJson = new StringBuilder();
            sbJson.Append("[");

            foreach (var item in departmentList)
            {
                sbJson.Append("{\"id\":\""+ item.DepartmentCode +"\", \"value\":\""+ item.DepartmentName + "\"},");
            }
            sbJson.Remove(sbJson.Length - 1, 1);
            sbJson.Append("]");

            return Json(sbJson.ToString(), JsonRequestBehavior.AllowGet);
        }

        public string Recursion(List<DepartmentModel> dt, int parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            List<DepartmentModel> rows = dt.Where(item => item.ParentId == parentId).ToList();
            if (rows.Count > 0)
            {
                sbJson.Append("[");
                for (int i = 0; i < rows.Count; i++)
                {
                    string childString = Recursion(dt, rows[i].Id);
                    if (!string.IsNullOrEmpty(childString))
                    {
                        //comboTree必须设置【id】和【text】，一个是id一个是显示值
                        sbJson.Append("{\"label\":\"" + rows[i].DepartmentName + "\",\"id\":\"" + rows[i].Id + "\",\"children\":");
                        sbJson.Append(childString);
                    }
                    else
                        sbJson.Append("{\"label\":\"" + rows[i].DepartmentName + "\",\"id\":\"" + rows[i].Id + "\"},");
                }
                sbJson.Remove(sbJson.Length - 1, 1);
                sbJson.Append("]},");
            }
            return sbJson.ToString();
        }
    }
}