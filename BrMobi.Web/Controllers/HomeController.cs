using System.Web.Mvc;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}