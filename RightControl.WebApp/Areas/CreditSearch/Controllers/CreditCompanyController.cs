using Newtonsoft.Json;
using RightControl.IService.CreditCompany;
using RightControl.Model;
using RightControl.Model.CreditSearch;
using RightControl.WebApp.Areas.Admin.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RightControl.WebApp.Areas.CreditSearch.Controllers
{
    public class CreditCompanyController : BaseController
    {
        // GET: CreditSearch/CreditCompany
        private readonly ICreditCompanyService creditCompanyService;
        private readonly IClaimFormService claimFormService;

        public CreditCompanyController(ICreditCompanyService _service, IClaimFormService _claimFormService)
        {
            creditCompanyService = _service;
            claimFormService = _claimFormService;
        }

        public ActionResult ResultVisit(int creditCompanyId)
        {
            ViewBag.Id = creditCompanyId;
            return View();
        }

        public ActionResult History(int creditCompanyId)
        {
            var result = claimFormService.ClaimHistory(creditCompanyId);
            if(result.Count == 0)
            {
                ViewBag.html = "<span>无认领历史<span>";
            }
            else
            {
                var html = "";
                foreach(var item in result)
                {
                    if (item.VisitingFeedback != 0)
                    {
                        var str = "";
                        if (item.VisitingFeedback == 1)
                        {
                            str = "未能联系到客户";
                        }
                        else if (item.VisitingFeedback == 2)
                        {
                            str = "已完成实地走访";
                        }
                        else if (item.VisitingFeedback == 3)
                        {
                            str = "电联无融资需要";
                        }
                        else if (item.VisitingFeedback == 4)
                        {
                            str = "已达成融资意向";
                        }
                        html += $@"<li class='layui-timeline-item'>
                                <i class='layui-icon layui-timeline-axis'></i>
                                <div class='layui-timeline-content layui-text'>
                                    <h3 class='layui-timeline-title'>企业走访反馈</h3>
                                    <p>
                                        走访人:{item.User.RealName}   柜员号: {item.User.UserName}
                                        <br>走访反馈:{str}
                                        <br>备注:{item.Remarks}
                                        <br>走访时间:{item.VisitingTime}
                                    </p>
                                </div>
                            </li>";
                    }

                    var name = "";
                    if(item.ContinueItem == 1)
                    {
                        name = "持续走访";
                    }
                    else
                    {
                        name = "企业认领";
                    }
                    html += $@"<li class='layui-timeline-item'>
                                <i class='layui-icon layui-timeline-axis'></i>
                                <div class='layui-timeline-content layui-text'>
                                    <h3 class='layui-timeline-title'>{name}</h3>
                                    <p>
                                        认领人:{item.User.RealName}   柜员号: {item.User.UserName}
                                        <br>认领时间:{item.ClaimTime}
                                    </p>
                                </div>
                            </li>";
                }
                ViewBag.html = html;
            }
            return View();
        }

        public ActionResult TaxpayerList()
        {
            var result = claimFormService.GetTaxpayerList(Operator.UserName);
            var html = "";
            if(result.Count == 0)
            {
                html = "<span>无数据</span>";
            }
            else
            {
                foreach (var item in result)
                {
                    html += $@"<div class='layui-colla-item'>
                        <h2 class='layui-colla-title'>{item.TaxpayerName}</h2>
                        <div class='layui-colla-content'>
                            <div class='layui-row'>
                                <div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>
                                    纳税人识别号
                                </div>
                                <div class='layui-col-xs8 layui-col-sm8 layui-col-md8'>
                                    {item.TaxpayerIdentifier}
                                </div>
                            </div>
                            <div class='layui-row'>
                                <div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>
                                    法定代表人姓名
                                </div>
                                <div class='layui-col-xs8 layui-col-sm8 layui-col-md8'>
                                    {item.LegalRepresentative}
                                </div>
                            </div>
                            <div class='layui-row'>
                                <div class='layui-col-xs4 layui-col-sm4 layui-col-md4'>
                                    经营地址
                                </div>
                                <div class='layui-col-xs8 layui-col-sm8 layui-col-md8'>
                                    {item.ProductionAddress}
                                </div>
                            </div>
                            <div class='layui-row'>
                                <div class='layui-col-xs12 layui-col-sm12 layui-col-md12'>
                                    <button class='layui-btn'>登记走访</button>
                                    <input type='hidden' value='{item.Id}' />
                                </div>
                            </div>
                        </div>
                    </div>";
                }
            }
            
            ViewBag.html = html;
            return View();
        }

        public ActionResult CreditCompanyList(CreditCompanyModel creditCompany, PageInfo pageInfo)
        {
            var result = creditCompanyService.GetCreditCompanyList(creditCompany, pageInfo, Operator.UserName);
            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ActionResult ClaimCompany(int CreditCompanyId)
        {
            var result = claimFormService.ClaimCreditCompany(CreditCompanyId, Operator.UserName);

            return Json(result);
        }

        public ActionResult VistingFeedback(VisitingConfirm obj)
        {
            var result = claimFormService.VisitingFeedback(obj) ? SuccessTip("填写反馈成功!") : ErrorTip("填写反馈失败,请重新尝试!");

            return Json(result);
        }
    }
}