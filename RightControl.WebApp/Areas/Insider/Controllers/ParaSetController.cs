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
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace RightControl.WebApp.Areas.Insider.Controllers
{
    public class ParaSetController : BaseController
    {
        public IRuleListService ruleListService { get; set; }
        public IParameterService parameterService { get; set; }
        public IModuleService moduleService { get; set; }

        public IInsiderWarnService insiderWarnService { get; set; }

        [HttpGet]
        public JsonResult List(RuleList filter, PageInfo pageInfo)
        {
            //将规则和参数数据整理返回
            JsonResult js = Json(ruleListService.GetListByFilter(filter, pageInfo));
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(js);
            JObject jo = JObject.Parse(json);

            IEnumerable<dynamic> dynamics = jo.Values().Values().Values().Children();
            dynamic[] arrays = dynamics.ToArray();

            string list = "[";
            int i = 0, j = 0;
            Parameter pa = new Parameter();
            PageInfo page = new PageInfo();
            page.limit = 10;
            page.page = 1;
            JsonResult jspara;
            string jsonpara;
            JObject jopara;
            dynamic[] arrayspara;

            for (i = 0; i < arrays.Length - 1; i++)
            {
                pa.R_No = arrays[i]["R_No"];

                jspara = Json(parameterService.GetListByFilter(pa, page));
                jsonpara = Newtonsoft.Json.JsonConvert.SerializeObject(jspara);
                jopara = JObject.Parse(jsonpara);

                arrayspara = jopara.Values().Values().Values().Children().ToArray();
                if (arrayspara.Length == 0)
                    list += "{\"M_Name\":\"" + arrays[i]["M_Name"] + "\",\"R_No\":" + arrays[i]["R_No"] + ",\"R_Name\":\"" + arrays[i]["R_Name"] + "\"},";
                else
                {
                    list += "{\"M_Name\":\"" + arrays[i]["M_Name"] + "\",\"R_No\":" + arrays[i]["R_No"] + ",\"R_Name\":\"" + arrays[i]["R_Name"] + "\",\"friend\": [";
                    for (j = 0; j < arrayspara.Length - 1; j++)
                    {
                        list += "{\"P_No\":" + arrayspara[j]["P_No"] + ",\"P_Name\":\"" + arrayspara[j]["P_Name"] + "\",\"P_Type\":\"" + arrayspara[j]["P_Type"] + "\",\"P_Max\":\"" + arrayspara[j]["P_Max"] + "\",\"P_Min\":\"" + arrayspara[j]["P_Min"] + "\"},";
                    }
                    list += "{\"P_No\":" + arrayspara[j]["P_No"] + ",\"P_Name\":\"" + arrayspara[j]["P_Name"] + "\",\"P_Type\":\"" + arrayspara[j]["P_Type"] + "\",\"P_Max\":\"" + arrayspara[j]["P_Max"] + "\",\"P_Min\":\"" + arrayspara[j]["P_Min"] + "\"}]},";
                }
            }

            pa.R_No = arrays[i]["R_No"];

            jspara = Json(parameterService.GetListByFilter(pa, page));
            jsonpara = Newtonsoft.Json.JsonConvert.SerializeObject(jspara);
            jopara = JObject.Parse(jsonpara);

            arrayspara = jopara.Values().Values().Values().Children().ToArray();
            if (arrayspara.Length == 0)
                list += "{\"M_Name\":\"" + arrays[i]["M_Name"] + "\",\"R_No\":" + arrays[i]["R_No"] + ",\"R_Name\":\"" + arrays[i]["R_Name"] + "\"}]";
            else
            {
                list += "{\"M_Name\":\"" + arrays[i]["M_Name"] + "\",\"R_No\":" + arrays[i]["R_No"] + ",\"R_Name\":\"" + arrays[i]["R_Name"] + "\",\"friend\": [";
                for (j = 0; j < arrayspara.Length - 1; j++)
                {
                    list += "{\"P_No\":" + arrayspara[j]["P_No"] + ",\"P_Name\":\"" + arrayspara[j]["P_Name"] + "\",\"P_Type\":\"" + arrayspara[j]["P_Type"] + "\",\"P_Max\":\"" + arrayspara[j]["P_Max"] + "\",\"P_Min\":\"" + arrayspara[j]["P_Min"] + "\"},";
                }
                list += "{\"P_No\":" + arrayspara[j]["P_No"] + ",\"P_Name\":\"" + arrayspara[j]["P_Name"] + "\",\"P_Type\":\"" + arrayspara[j]["P_Type"] + "\",\"P_Max\":\"" + arrayspara[j]["P_Max"] + "\",\"P_Min\":\"" + arrayspara[j]["P_Min"] + "\"}]}]";
            }

            var result = new { code = 0, count = arrays.Length, data = list };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public SelectList ModuleList { get { return new SelectList(moduleService.GetModule(), "No", "Name"); } }

        public SelectList RuleList { get { return new SelectList(ruleListService.GetRule(), "R_No", "R_Name"); } }

        public ActionResult Add()
        {
            ViewBag.ModuleNo = ModuleList;
            return View();
        }

        [HttpPost]
        public ActionResult Add(RuleList obj)
        {
            PageInfo page = new PageInfo();
            page.limit = 10;
            page.page = 1;
            Module module = new Module();
            module.No = obj.M_No;

            JsonResult js = Json(moduleService.GetListByFilter(module, page));
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(js);
            JObject jo = JObject.Parse(json);
            dynamic[] arr = jo.Values().Values().Values().Children().ToArray();

            obj.M_Name = arr[0]["Name"];
            obj.CreateOn = DateTime.Now;
            var result = ruleListService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.ModuleNo = ModuleList;
            var obj = ruleListService.GetByWhere(" where R_No=@R_No", new { R_No = id }).ToList()[0];
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(RuleList obj)
        {
            PageInfo page = new PageInfo();
            page.limit = 10;
            page.page = 1;
            Module module = new Module();
            module.No = obj.M_No;

            JsonResult js = Json(moduleService.GetListByFilter(module, page));
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(js);
            JObject jo = JObject.Parse(json);
            dynamic[] arr = jo.Values().Values().Values().Children().ToArray();

            obj.M_Name = arr[0]["Name"];

            var result = ruleListService.UpdateModel(obj) ? SuccessTip("编辑成功!") : ErrorTip("编辑失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            var result = ruleListService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int Id)
        {
            var obj = ruleListService.GetByWhere(" where R_No=@R_No", new { R_No = Id }).ToList()[0];
            return View(obj);
        }

        public ActionResult ParaAdd(int id)
        {
            ViewBag.RuleNo = new SelectList(ruleListService.GetRuleName(id), "R_No", "R_Name");
            return View();
        }

        [HttpPost]
        public ActionResult ParaAdd(Parameter obj)
        {
            RuleList rule = ruleListService.GetByWhere(" where R_No=@R_No", new { R_No = obj.R_No }).ToList()[0];
            obj.R_Name = rule.R_Name;
            obj.M_No = rule.M_No;
            obj.M_Name = rule.M_Name;
            obj.CreateOn = DateTime.Now;
            var result = parameterService.CreateModel(obj) ? SuccessTip("新增成功!") : ErrorTip("新增失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ParaEdit(Parameter para)
        {
            //para传送过来会缺少父表格的规则模块信息
            Parameter parameter = parameterService.GetByWhere(" where P_No=@P_No", new { P_No = para.P_No }).ToList()[0];
            para.R_No = parameter.R_No;
            para.R_Name = parameter.R_Name;
            para.M_No = parameter.M_No;
            para.M_Name = parameter.M_Name;
            para.CreateOn = parameter.CreateOn;

            var result = parameterService.UpdateModel(para) ? SuccessTip("编辑成功!") : ErrorTip("编辑失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ParaDel(int Id)
        {
            var result = parameterService.DeleteModel(Id) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Warn()
        {
            return View();
        }

        [HttpGet]
        public JsonResult WarnList(YG_InsiderWarn filter, PageInfo pageInfo)
        {
            var result = insiderWarnService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}