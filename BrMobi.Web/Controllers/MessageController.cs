using System;
using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Web.Attributes;

namespace BrMobi.Web.Controllers
{
    [BrMobiAuthorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService messageService;
        private readonly IAccountService accountService;

        public MessageController(IMessageService messageService,
                                 IAccountService accountService)
        {
            this.messageService = messageService;
            this.accountService = accountService;
        }

        public JsonResult Create(string text, int userId)
        {
            var message = new Message()
            {
                CreatedOn = DateTime.Now,
                From = LoggedUser,
                Text = text,
                To = accountService.GetUser(userId)
            };

            message = messageService.Create(message);

            return Json(message);
        }

        public JsonResult Remove(int id)
        {
            messageService.Remove(id);
            return Json(null);
        }
    }
}