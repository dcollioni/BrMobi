using System;
using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.Entities;
using BrMobi.Core.Enums;
using BrMobi.Web.Attributes;
using Facebook;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class HomeController : BaseController
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var accessToken = Session["accessToken"].ToString();
            var client = new FacebookClient(accessToken);
            dynamic facebookUser = client.Get("me");

            ViewBag.UserName = facebookUser.username;
            ViewBag.AccessToken = accessToken;

            var brMobiUser = userService.GetByFacebookId(facebookUser.id);
            if (brMobiUser == null)
            {
                var user = new User
                {
                    FacebookId = facebookUser.id,
                    Name = facebookUser.name,
                    FirstName = facebookUser.first_name,
                    LastName = facebookUser.last_name,
                    Link = facebookUser.link,
                    UserName = facebookUser.username,
                    Gender = facebookUser.gender == "male" ? Gender.Male : Gender.Female,
                    Locale = facebookUser.locale,
                    AgeRange = facebookUser.age_range,
                    CreatedOn = DateTime.Now
                };
                userService.Create(user);
            }
            LoggedUser = brMobiUser;
            return View();
        }
    }
};