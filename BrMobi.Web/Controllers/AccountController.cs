using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.Service;
using BrMobi.Web.Models;
using System.IO;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Drawing;
using BrMobi.ApplicationServices.ServiceInterfaces.Evaluation;

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
                    Password = model.Password.Trim()
                };

                OperationStatus operationStatus;
                accountService.CreateUser(user, out operationStatus);

                if (operationStatus.Success)
                {
                    LoggedUser = user;
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

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult ChangePicture(object picture)
        {
            if (LoggedUser != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Request.Files[0].InputStream.CopyTo(ms);
                    byte[] array = ms.ToArray();

                    LoggedUser.Picture = Convert.ToBase64String(array);
                    LoggedUser = accountService.ChangePicture(LoggedUser);
                }
            }

            return RedirectToAction("Index", "Profile");
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
