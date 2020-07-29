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
    public class InsiderListController : BaseController
    {
        public IInsiderListService insiderListService { get; set; }
        public IUserService userService { get; set; }

        public IInsiderRltsListService insiderrltsservice { get; set; }

        [HttpGet]
        public JsonResult List(YG_InsiderList filter, PageInfo pageInfo)
        {
            var result = insiderListService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(YG_InsiderList obj)
        {
            obj.CreateOn = DateTime.Now;
            var result = insiderListService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RelationGraph(int Id)
        {
            var obj = insiderListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public JsonResult GetRelationData(string Post, string RepartyNm)
        {
            YG_InsiderRltsList rltsList = new YG_InsiderRltsList();
            rltsList.InsiderPs = Post;
            rltsList.InsiderNm = RepartyNm;

            PageInfo pageInfo = new PageInfo();
            pageInfo.limit = 50;
            pageInfo.page = 1;

            JsonResult js = Json(insiderrltsservice.GetListByFilter(rltsList, pageInfo));
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(js);
            JObject jo = JObject.Parse(json);


            IEnumerable<dynamic> dynamics = jo.Values().Values().Values().Children();
            int count = dynamics.Count();
            dynamic[] arrays = dynamics.ToArray();

            int i;
            var data = " [{\"name\": \"" + RepartyNm + "\",\"des\": \"" + RepartyNm + "\",\"symbolSize\": 70,\"category\": 0},";
            var link = "[";
            for (i = 0; i < count - 1; i++)
            {
                data += "{ \"name\": \"" + arrays[i]["RltsNm"] + "\",\"des\": \"" + arrays[i]["RltsNm"] + "\",\"symbolSize\": 50,\"category\": 1},";
                link += "{\"source\": \"" + RepartyNm + "\",\"target\": \"" + arrays[i]["RltsNm"] + "\",\"name\": \"" + arrays[i]["Relationship"] + "\",\"des\": \"" + arrays[i]["Relationship"] + "\"}, ";
            }

            data += "{ \"name\": \"" + arrays[i]["RltsNm"] + "\",\"des\": \"" + arrays[i]["RltsNm"] + "\",\"symbolSize\": 50,\"category\": 1}]";
            link += "{\"source\": \"" + RepartyNm + "\",\"target\": \"" + arrays[i]["RltsNm"] + "\",\"name\": \"" + arrays[i]["Relationship"] + "\",\"des\": \"" + arrays[i]["Relationship"] + "\"}]";

            return Json(new
            {
                data = data,
                link = link
            });
        }

        public ActionResult Detail(int Id)
        {
            var obj = insiderListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var obj = insiderListService.GetByWhere(" where Id=@Id", new { Id = Id }).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(YG_InsiderList obj)
        {
            var result = insiderListService.UpdateModel(obj) ? SuccessTip("编辑成功!") : ErrorTip("编辑失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            var result = insiderListService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
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

                string path = Server.MapPath("/Upload/file/insider/");    //获取保存目录的物理路径
                //生成新文件的名称，guid保证某一时刻内文件名唯一（文件不会被覆盖）
                string fileNewName = Guid.NewGuid().ToString();
                FileUrl = path + fileNewName + extName;
                file.SaveAs(FileUrl);    //SaveAs将文件保存到指定文件夹中

                designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(FileUrl);
                sheet = designer.Workbook.Worksheets[0];  

                if(sheet.Name!="内部人")
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
                YG_InsiderList obj = new YG_InsiderList();
                if (!string.IsNullOrEmpty(dr["RepartyNm"].ToString()))
                    obj.RepartyNm = dr["RepartyNm"].ToString();
                if (!string.IsNullOrEmpty(dr["ReNature"].ToString()))
                    obj.ReNature = dr["ReNature"].ToString();
                if (!string.IsNullOrEmpty(dr["Department"].ToString()))
                    obj.Department = dr["Department"].ToString();
                if (!string.IsNullOrEmpty(dr["Post"].ToString()))
                    obj.Post = dr["Post"].ToString();
                if (!string.IsNullOrEmpty(dr["IdentityCd"].ToString()))
                    obj.IdentityCd = dr["IdentityCd"].ToString();
                obj.CreateOn = DateTime.Now;
                var result = insiderListService.CreateModel(obj);

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