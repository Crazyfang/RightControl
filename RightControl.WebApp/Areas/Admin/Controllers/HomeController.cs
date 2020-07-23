using System.Web.Mvc;

namespace RightControl.WebApp.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public override ActionResult Index(int? id)
        {
            ViewBag.RealName= Operator == null ? "" : Operator.RealName;
            ViewBag.HeadShot = Operator == null ? "" : Operator.HeadShot;
            
            if (Operator == null || Operator.RoleId == 1)
            {
                ViewBag.ShowPage = 1;
            }
            else
            {
                ViewBag.ShowPage = 0;
            }
            return View(GetWebSiteInfo());
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Index_old(int? id)
        {
            ViewBag.RealName = Operator == null ? "" : Operator.RealName;
            ViewBag.HeadShot = Operator == null ? "" : Operator.HeadShot;

            if (Operator == null || Operator.RoleId == 1)
            {
                ViewBag.ShowPage = 1;
            }
            else
            {
                ViewBag.ShowPage = 0;
            }
            return View(GetWebSiteInfo());
        }
    }
}