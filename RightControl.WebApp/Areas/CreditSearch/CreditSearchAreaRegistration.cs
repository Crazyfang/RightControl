using System.Web.Mvc;

namespace RightControl.WebApp.Areas.CreditSearch
{
    public class CreditSearchAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CreditSearch";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CreditSearch_default",
                "CreditSearch/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}