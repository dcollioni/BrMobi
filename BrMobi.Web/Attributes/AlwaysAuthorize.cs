using System.Web.Mvc;
using System.Web.Routing;

namespace BrMobi.Web.Attributes
{
    public class AlwaysAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }
    }
}