using System.Web.Mvc;
using System.Web.Routing;

namespace BrMobi.Web.Attributes
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session == null || session["admin"] == null)
            {
                var routeValueDictionary = new RouteValueDictionary(new { controller = "Admin", action = "Index" });
                filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}