using System.Web.Mvc;

namespace RightControl.WebApp.Areas.RecordTrancation
{
    public class RecordTrancationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RecordTrancation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RecordTrancation_default",
                "RecordTrancation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}