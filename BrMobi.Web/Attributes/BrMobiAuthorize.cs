using System.Web.Mvc;
using System.Web.Routing;

namespace BrMobi.Web.Attributes
{
    public class BrMobiAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (session != null && session["User"] != null) return;

            var route = new RouteValueDictionary(new { controller = "Acesso" });
            filterContext.Result = new RedirectToRouteResult(route);
        }
    }
}