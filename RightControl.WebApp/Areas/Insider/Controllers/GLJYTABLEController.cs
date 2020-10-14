using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RightControl.WebApp.Areas.Admin.Controllers;
using RightControl.Model;
using RightControl.Model.Insider;
using RightControl.IService;
using RightControl.IService.Insider;
using Aspose.Cells;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RightControl.WebApp.Areas.Insider.Controllers
{
    public class GLJYTABLEController : BaseController
    {
        // GET: Insider/GLJYTABLE

        public IGLJYTABLEService GLJYTABLEService { get; set; }

        [HttpGet]
        public JsonResult List(YG_GLJYTABLE filter, PageInfo pageInfo)
        {
            var result = GLJYTABLEService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Export(string data_date)
        {
            //创建Excel文件的对象  
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet  
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            YG_GLJYTABLE gliy = new YG_GLJYTABLE();
            gliy.DATA_DATE = data_date;
            PageInfo pageInfo = new PageInfo();
            pageInfo.limit = 5000;
            pageInfo.page = 1;

            JsonResult js = Json(GLJYTABLEService.GetListByFilter(gliy, pageInfo));
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(js);
            JObject jo = JObject.Parse(json);
            IEnumerable<dynamic> dynamics = jo.Values().Values().Values().Children();
            int count = dynamics.Count();
            dynamic[] arrays = dynamics.ToArray();

            //给sheet1添加第一行的头部标题  
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("数据时间");
            row1.CreateCell(1).SetCellValue("关联方名称");
            row1.CreateCell(2).SetCellValue("证件类型");
            row1.CreateCell(3).SetCellValue("证件代码");
            row1.CreateCell(4).SetCellValue("授信总额");
            row1.CreateCell(5).SetCellValue("贷款余额");
            row1.CreateCell(6).SetCellValue("信用贷款");
            row1.CreateCell(7).SetCellValue("贷记卡");
            row1.CreateCell(8).SetCellValue("互联网金融贷款余额");
            row1.CreateCell(9).SetCellValue("票据承兑");
            row1.CreateCell(10).SetCellValue("票据贴现");
            row1.CreateCell(11).SetCellValue("担保");
            row1.CreateCell(12).SetCellValue("保证金、质押的银行存单和国债余额");
            row1.CreateCell(13).SetCellValue("授信余额");

            //将数据逐步写入sheet1各个行  
            for (int i = 0; i < count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(arrays[i]["DATA_DATE"].ToString().Split(' ')[0]);
                rowtemp.CreateCell(1).SetCellValue(arrays[i]["INSIDERNM"].ToString());
                rowtemp.CreateCell(2).SetCellValue(arrays[i]["CERTTYPE"].ToString());
                rowtemp.CreateCell(3).SetCellValue(arrays[i]["CERT_NO"].ToString());
                rowtemp.CreateCell(4).SetCellValue(arrays[i]["SHOUXIN"].ToString());
                rowtemp.CreateCell(5).SetCellValue(arrays[i]["LOAN"].ToString());
                rowtemp.CreateCell(6).SetCellValue(arrays[i]["XINYONG_LOAN"].ToString());
                rowtemp.CreateCell(7).SetCellValue(arrays[i]["CRET_CARD"].ToString());
                rowtemp.CreateCell(8).SetCellValue(arrays[i]["INTER_FINANCE"].ToString());
                rowtemp.CreateCell(9).SetCellValue(arrays[i]["BILL_ACCEPT"].ToString());
                rowtemp.CreateCell(10).SetCellValue(arrays[i]["BILL_DISCOUNTED"].ToString());
                rowtemp.CreateCell(11).SetCellValue(arrays[i]["DANBAO"].ToString());
                rowtemp.CreateCell(12).SetCellValue(arrays[i]["ZHIYA"].ToString());
                rowtemp.CreateCell(13).SetCellValue(arrays[i]["GRANT_BALANCE"].ToString());
            }

            string path = Server.MapPath("/Download/GLJYTABLE/");    //获取保存目录的物理路径
            DateTime dt = DateTime.Now;
            string dateTime = dt.ToString("yyyyMMddHHmmssfff");
            string FileName = "关联交易情况表" + dateTime + ".xls";
            string saveFileName = path + FileName;

            try
            {
                using (FileStream fs = new FileStream(saveFileName, FileMode.Create, FileAccess.Write))
                {
                    book.Write(fs);  //写入文件
                    book.Close();  //关闭
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('错误信息：" + ex.Message + "');</script>");
            }

            return Content("/Download/GLJYTABLE/" + FileName);
        }
    }
}