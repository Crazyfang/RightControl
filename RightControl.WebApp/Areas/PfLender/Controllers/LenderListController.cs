using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RightControl.WebApp.Areas.Admin.Controllers;
using RightControl.IService;
using RightControl.IService.PfLender;
using RightControl.Model;
using RightControl.Model.PfLender;
using System.IO;
using Aspose.Cells;
using System.Data;
using RightControl.Common;

namespace RightControl.WebApp.Areas.PfLender.Controllers
{
    public class LenderListController : BaseController
    {
        public ILenderListService lenderListService { get; set; }

        public IUserService userService { get; set; }

        public ILenderListLoanService LenderListLoanService { get; set; }

        public ILenderListDepService LenderListDepService { get; set; }

        public ILenderListDepFlowService LenderListDepFlowService { get; set; }

        [HttpGet]
        public JsonResult List(LenderList filter, PageInfo pageInfo)
        {
            var result = lenderListService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            string filePath = "/Download/Template.xls"; 
            return Content(filePath);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(LenderList obj)
        {
            obj.CreateOn = DateTime.Now;
            var result = lenderListService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Import()
        {
            string FileUrl;
            try
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string extName = Path.GetExtension(file.FileName).ToLower();    //获取文件名后缀

                if (string.IsNullOrEmpty(file.FileName) || extName != ".xls")
                {
                    return Json(new
                    {
                        Result  = false,
                        Data = "请选择一个EXCEL文件！"
                    });
                }

                string path = Server.MapPath("/Upload/file/");    //获取保存目录的物理路径
                //生成新文件的名称，guid保证某一时刻内文件名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                FileUrl = path + fileNewName + extName;
                file.SaveAs(FileUrl);    //SaveAs将文件保存到指定文件夹中
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    Result  = false,
                    Data = exception.Message
                });
            }

            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(FileUrl);
            Worksheet sheet = designer.Workbook.Worksheets[0];
            Cells cells = sheet.Cells;
            DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);

            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new
                {
                    Result  = false,
                    Data = "该文件无具体数据项!"
                });
            }

            dt.Rows.RemoveAt(0);     //去掉表格第一行

            foreach (DataRow dr in dt.Rows)
            {
                LenderList obj = new LenderList();
                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    obj.Name = dr["Name"].ToString();
                if (!string.IsNullOrEmpty(dr["IdentityCd"].ToString()))
                    obj.IdentityCd = dr["IdentityCd"].ToString();
                if (!string.IsNullOrEmpty(dr["Place"].ToString()))
                    obj.Place = dr["Place"].ToString();
                if (!string.IsNullOrEmpty(dr["LawCase"].ToString()))
                    obj.LawCase = int.Parse(dr["LawCase"].ToString());
                if (!string.IsNullOrEmpty(dr["Amount"].ToString()))
                    obj.Amount = double.Parse(dr["Amount"].ToString());
                obj.CreateOn = DateTime.Now;
                var result = lenderListService.CreateModel(obj);

                if (result == false)
                {
                    return Json(new
                    {
                        Result  = false,
                        Data = "上传失败!"
                    });
                }
            }

            return Json(new
            {
                Result  = true,
                Data = "上传成功!"
            });
        }
        public ActionResult Detail(int Id)
        {
            var obj = lenderListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        public ActionResult Delete(int Id)
        {
            var result = lenderListService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var obj = lenderListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(LenderList obj)
        {
            var result = lenderListService.UpdateModel(obj) ? SuccessTip("编辑成功!") : ErrorTip("编辑失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Deposit()
        {
            return View();
        }

        public ActionResult DepFlow()
        {
            return View();
        }

        public ActionResult Loan()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetLenderListLoan(LenderList_Loan filter, PageInfo pageInfo)
        {
            var result = LenderListLoanService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLenderListDep(LenderList_Dep filter, PageInfo pageInfo)
        {
            var result = LenderListDepService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLenderListDepFlow(LenderList_DepFlow filter, PageInfo pageInfo)
        {
            var result = LenderListDepFlowService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}