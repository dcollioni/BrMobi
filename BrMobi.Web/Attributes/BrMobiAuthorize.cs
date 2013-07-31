using System.Web.Mvc;
using System.Web.Routing;

namespace BrMobi.Web.Attributes
{
    public class BrMobiAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || string.IsNullOrWhiteSpace((session["accessToken"] ?? string.Empty).ToString()))
            {
                var routeValueDictionary = new RouteValueDictionary(new { controller = "Account", action = "Welcome" });
                filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}