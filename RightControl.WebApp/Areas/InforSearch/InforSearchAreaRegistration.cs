using System.Web.Mvc;

namespace RightControl.WebApp.Areas.InforSearch
{
    public class InforSearchAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InforSearch";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InforSearch_default",
                "InforSearch/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}