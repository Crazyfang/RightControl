using System.Web.Mvc;

namespace RightControl.WebApp.Areas.PfLender
{
    public class PfLenderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PfLender";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PfLender_default",
                "PfLender/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}