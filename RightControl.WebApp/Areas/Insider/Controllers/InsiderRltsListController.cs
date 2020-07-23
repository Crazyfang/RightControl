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

namespace RightControl.WebApp.Areas.Insider.Controllers
{
    public class InsiderRltsListController : BaseController
    {
        public IInsiderListService insiderListService { get; set; }
        public IInsiderRltsListService insiderrltsListService { get; set; }
        public IUserService userService { get; set; }

        [HttpPost]
        public JsonResult GetInsiderList(string Department)
        {
            IEnumerable<dynamic> list = insiderListService.GetInsiderList(Department);
            var result = new { code = 0, count = list.Count(), data = list };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult List(YG_InsiderRltsList filter, PageInfo pageInfo)
        {
            var result = insiderrltsListService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(YG_InsiderRltsList obj)
        {
            obj.CreateOn = DateTime.Now;
            var result = insiderrltsListService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int Id)
        {
            var obj = insiderrltsListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var obj = insiderrltsListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(YG_InsiderRltsList obj)
        {
            var result = insiderrltsListService.UpdateModel(obj) ? SuccessTip("编辑成功!") : ErrorTip("编辑失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            var result = insiderrltsListService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download()
        {
            string filePath = "/Download/Template_in.xls";
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

                string path = Server.MapPath("/Upload/file/insiderrlts/");    //获取保存目录的物理路径
                //生成新文件的名称，guid保证某一时刻内文件名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                FileUrl = path + fileNewName + extName;
                file.SaveAs(FileUrl);    //SaveAs将文件保存到指定文件夹中

                designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(FileUrl);
                sheet = designer.Workbook.Worksheets[1];

                if (sheet.Name != "内部人近亲属")
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
            DataTable dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn, true);

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
            dt.Rows.RemoveAt(0);     //去掉表格第三行

            foreach (DataRow dr in dt.Rows)
            {
                if (string.IsNullOrEmpty(dr["Department"].ToString()))
                    continue;

                YG_InsiderRltsList obj = new YG_InsiderRltsList();
                if (!string.IsNullOrEmpty(dr["Department"].ToString()))
                    obj.Department = dr["Department"].ToString();
                if (!string.IsNullOrEmpty(dr["InsiderNm"].ToString()))
                    obj.InsiderNm = dr["InsiderNm"].ToString();
                if (!string.IsNullOrEmpty(dr["RltsNm"].ToString()))
                    obj.RltsNm = dr["RltsNm"].ToString();
                if (!string.IsNullOrEmpty(dr["IdentityCd"].ToString()))
                    obj.IdentityCd = dr["IdentityCd"].ToString();
                if (!string.IsNullOrEmpty(dr["Relationship"].ToString()))
                    obj.Relationship = dr["Relationship"].ToString();
                if (!string.IsNullOrEmpty(dr["CompanyNm"].ToString()))
                    obj.CompanyNm = dr["CompanyNm"].ToString();
                if (!string.IsNullOrEmpty(dr["SocialCreditCode"].ToString()))
                    obj.SocialCreditCode = dr["SocialCreditCode"].ToString();
                if (!string.IsNullOrEmpty(dr["Controller"].ToString()))
                    obj.Controller = dr["Controller"].ToString();
                if (!string.IsNullOrEmpty(dr["ControllerRlts"].ToString()))
                    obj.ControllerRlts = dr["ControllerRlts"].ToString();
                obj.CreateOn = DateTime.Now;
                var result = insiderrltsListService.CreateModel(obj);

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