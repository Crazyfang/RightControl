using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RightControl.IService;
using RightControl.IService.Insider;
using RightControl.Model;
using RightControl.Model.Insider;
using RightControl.WebApp.Areas.Admin.Controllers;
using System.IO;
using Aspose.Cells;
using System.Data;
using RightControl.Common;
using Newtonsoft.Json.Linq;

namespace RightControl.WebApp.Areas.Insider.Controllers
{
    public class InsiderFaRenListController : BaseController
    {
        public IInsiderFaRenListService insiderFaRenListService { get; set; }
        public IUserService userService { get; set; }

        [HttpGet]
        public JsonResult List(YG_InsiderFaRenList filter, PageInfo pageInfo)
        {
            var result = insiderFaRenListService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(YG_InsiderFaRenList obj)
        {
            obj.CreateOn = DateTime.Now;
            var result = insiderFaRenListService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var obj = insiderFaRenListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(YG_InsiderFaRenList obj)
        {
            var result = insiderFaRenListService.UpdateModel(obj) ? SuccessTip("编辑成功!") : ErrorTip("编辑失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int Id)
        {
            var obj = insiderFaRenListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        public ActionResult Delete(int Id)
        {
            var result = insiderFaRenListService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            string filePath = "/Download/Template_inFaRen.xls";
            return Content(filePath);
        }

        [HttpPost]
        public JsonResult Import()
        {
            string FileUrl;
            WorkbookDesigner designer;
            Worksheet sheet;
            try
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                string extName = Path.GetExtension(file.FileName).ToLower();    //获取文件名后缀

                if (string.IsNullOrEmpty(file.FileName) || extName != ".xls")
                {
                    return Json(new
                    {
                        Result = false,
                        Data = "请选择一个EXCEL文件！"
                    });
                }

                string path = Server.MapPath("/Upload/file/insiderFaRen/");    //获取保存目录的物理路径
                //生成新文件的名称，guid保证某一时刻内文件名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                FileUrl = path + fileNewName + extName;
                file.SaveAs(FileUrl);    //SaveAs将文件保存到指定文件夹中

                designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(FileUrl);
                sheet = designer.Workbook.Worksheets[0];

                if (sheet.Name != "法人")
                {
                    return Json(new
                    {
                        Result = false,
                        Data = "请选择模板导入！"
                    });
                }
            }
            catch (Exception exception)
            {
                return Json(new
                {
                    Result = false,
                    Data = exception.Message
                });
            }

            Cells cells = sheet.Cells;
            DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);

            if (dt == null || dt.Rows.Count == 0)
            {
                return Json(new
                {
                    Result = false,
                    Data = "该文件无具体数据项!"
                });
            }

            dt.Rows.RemoveAt(0);     //去掉表格第一行
            dt.Rows.RemoveAt(0);     //去掉表格第二行

            foreach (DataRow dr in dt.Rows)
            {
                YG_InsiderFaRenList obj = new YG_InsiderFaRenList();
                if (!string.IsNullOrEmpty(dr["FaRenNm"].ToString()))
                    obj.FaRenNm = dr["FaRenNm"].ToString();
                if (!string.IsNullOrEmpty(dr["ReKind"].ToString()))
                    obj.ReKind = dr["ReKind"].ToString();
                if (!string.IsNullOrEmpty(dr["ReportUnit"].ToString()))
                    obj.ReportUnit = dr["ReportUnit"].ToString();
                if (!string.IsNullOrEmpty(dr["Relationship"].ToString()))
                    obj.Relationship = dr["Relationship"].ToString();
                if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                    obj.Description = dr["Description"].ToString();
                obj.CreateOn = DateTime.Now;
                var result = insiderFaRenListService.CreateModel(obj);

                if (result == false)
                {
                    return Json(new
                    {
                        Result = false,
                        Data = "上传失败!"
                    });
                }
            }

            return Json(new
            {
                Result = true,
                Data = "上传成功!"
            });
        }
    }
}