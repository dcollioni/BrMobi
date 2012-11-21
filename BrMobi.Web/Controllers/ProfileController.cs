using System;
using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.Enums;
using BrMobi.Core.ViewModels;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class ProfileController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IStateService stateService;
        private readonly ICityService cityService;
        private readonly IMessageService messageService;

        public ProfileController(IAccountService accountService,
                                 IStateService stateService,
                                 ICityService cityService,
                                 IMessageService messageService)
        {
            this.accountService = accountService;
            this.stateService = stateService;
            this.cityService = cityService;
            this.messageService = messageService;
        }

        public ActionResult Index(int? id)
        {
            id = id ?? LoggedUser.Id;
            var user = accountService.GetUser(id.Value);

            ViewBag.User = Convert(user);

            ViewBag.States = stateService.ListAll();

            if (user.City != null)
            {
                ViewBag.Cities = cityService.ListAll(user.City.State.Id);
            }

            ViewBag.Messages = messageService.ListReceived(id.Value);
            ViewBag.Relationship = accountService.GetRelationship(id.Value);

            return View();
        }

        public JsonResult ListCities(int stateId)
        {
            var cities = cityService.ListAll(stateId);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(UserViewModel viewModel)
        {
            var user = new User()
            {
                Id = LoggedUser.Id,
                BirthDate = viewModel.BirthDate,
                City = viewModel.CityId > 0 ? cityService.Get(viewModel.CityId) : null,
                FacebookLink = viewModel.FacebookLink,
                Gender = !string.IsNullOrEmpty(viewModel.Gender) ? (Gender)(int.Parse(viewModel.Gender)) : new Gender?(),
                Name = viewModel.Name
            };

            LoggedUser = accountService.UpdateUser(user);

            return RedirectToAction("Index");
        }

        private UserViewModel Convert(User user)
        {
            var viewModel = new UserViewModel()
            {
                FacebookLink = user.FacebookLink,
                Gender = user.Gender.HasValue ? user.Gender == Gender.Female ? "Feminino" : "Masculino" : null,
                Id = user.Id,
                Name = user.Name,
                Picture = user.Picture,
                CityName = user.City != null ? user.City.Name : null,
                StateCode = user.City != null ? user.City.State.Code : null,
                BirthDate = user.BirthDate,
                CityId = user.City != null ? user.City.Id : 0,
                StateId = user.City != null ? user.City.State.Id : 0
            };

            if (user.BirthDate.HasValue)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - user.BirthDate.Value.Year;
                if (user.BirthDate.Value > today.AddYears(-age)) age--;

                viewModel.Age = age;
            }

            return viewModel;
        }
    }
}