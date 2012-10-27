using System;
using System.IO;
using System.Web.Mvc;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class HomeController : BaseController
    {
        readonly static string YapFileName = Path.Combine(
                               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                               "BrMobi.yap");

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}