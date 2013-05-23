using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.ApplicationServices.ServiceInterfaces.Evaluation;
using BrMobi.Core;
using BrMobi.Core.Service;
using BrMobi.Web.Models;
using Facebook;

namespace BrMobi.Web.Controllers
{
    public class AccountController : BaseController
    {
        private IAccountService accountService;
        private IEvaluationService evaluationService;

        public AccountController(IAccountService accountService,
                                 IEvaluationService evaluationService)
        {
            this.accountService = accountService;
            this.evaluationService = evaluationService;
        }

        public ActionResult Access()
        {
            if (LoggedUser != null)
            {
                return RedirectToAction("Index", "Home");
            }


            //var client = new Facebook.FacebookClient();
            //dynamic me = client.Get("daniel.wermann");

            var accessToken = "597380940272164|u_09KEz6dqgXM0bSG224ROh-10c";
            var client = new FacebookClient(accessToken);
            dynamic me = client.Get("daniel.wermann");
            string aboutMe = me.about;

            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            if (LoggedUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = accountService.GetUser(model.Email, model.Password);

                if (user != null)
                {
                    LoggedUser = user;
                    CanEvaluate = evaluationService.CanEvaluate(LoggedUser);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewData["LogonError"] = "O e-mail e a senha informados estão incorretos.";
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            LoggedUser = null;

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Access(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    CreatedOn = DateTime.Now,
                    Email = model.Email.Trim(),
                    Name = model.Name.Trim(),
                    Password = model.Password.Trim(),
                    Picture = "/UserImages/user.png"
                };

                OperationStatus operationStatus;
                accountService.CreateUser(user, out operationStatus);

                if (operationStatus.Success)
                {
                    LoggedUser = user;
                    CanEvaluate = true;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var message in operationStatus.Messages)
                    {
                        ViewBag.EmailError = message;
                    }
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    ModelState value;
                    ModelState.TryGetValue(key, out value);

                    if (value.Errors.Count > 0)
                    {
                        var keyError = string.Format("{0}Error", key);
                        ViewData[keyError] = value.Errors.First().ErrorMessage;
                    }
                }
            }

            return View(model);
        }

        public ActionResult ResetPassword(bool? success)
        {
            if (success.HasValue)
            {
                ViewBag.Success = success.Value;
            }

            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string email)
        {
            var success = true;

            try
            {
                var smtp = new SmtpClient();
                smtp.Host = "smtp.mailgun.org";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("postmaster@app10596.mailgun.org", "8s21znoe1rq0");
                
                var newPassword = Guid.NewGuid().ToString().Split('-')[0];

                var body = string.Format("<p>Você solicitou uma nova senha ao BrMobi.</p> <p>Acesse sua conta utilizando seu e-mail e a sua nova senha: <b>{0}</b>.</p>", newPassword);

                MailMessage message = new MailMessage("BrMobi@brmobi.apphb.com",
                                                      email,
                                                      "Redefinição de senha",
                                                      body);

                message.IsBodyHtml = true;
                
                smtp.Send(message);
                accountService.UpdateUserPassword(email, newPassword);
            }
            catch
            {
                success = false;
            }

            return Redirect("/RedefinirSenha/" + success);
        }

        public ActionResult ChangePicture(object picture)
        {
            if (LoggedUser != null)
            {
                var file = Request.Files[0];

                var imageFormat = file.FileName.Split('.').Last();
                var imagePath = Server.MapPath("~/UserImages/");
                file.SaveAs(string.Concat(imagePath, LoggedUser.Id, ".", imageFormat));

                LoggedUser.Picture = string.Concat("/UserImages/", LoggedUser.Id, ".", imageFormat);
                LoggedUser = accountService.ChangePicture(LoggedUser);
            }
            
            return RedirectToAction("Index", "Profile");
        }
    }
}