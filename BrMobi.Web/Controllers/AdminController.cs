using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Web.Attributes;
using System.Text;
using System.Security.Cryptography;
using System.Web.Configuration;

namespace BrMobi.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IPreUserService preUserService;

        public AdminController(IPreUserService preUserService)
        {
            this.preUserService = preUserService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var usernameBytes = Encoding.Unicode.GetBytes(username);
            var usernameMd5Bytes = new MD5Cng().ComputeHash(usernameBytes);
            var usernameMd5String = string.Join(",", usernameMd5Bytes);

            var passwordBytes = Encoding.Unicode.GetBytes(password);
            var passwordMd5Bytes = new MD5Cng().ComputeHash(passwordBytes);
            var passwordMd5String = string.Join(",", passwordMd5Bytes);

            var usernameValue = WebConfigurationManager.AppSettings["Username"];
            var passwordValue = WebConfigurationManager.AppSettings["Password"];

            if (usernameMd5String == usernameValue && passwordMd5String == passwordValue)
            {
                Session["Admin"] = true;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Logoff()
        {
            Session.Remove("Admin");
            return RedirectToAction("Index");
        }

        [AdminAuthorizeAttribute]
        public ActionResult PreUsers()
        {
            ViewBag.PreUsers = preUserService.GetAll();
            return View();
        }
    }
}