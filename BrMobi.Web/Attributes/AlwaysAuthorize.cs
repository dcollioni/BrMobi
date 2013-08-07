using System.Web.Mvc;

namespace BrMobi.Web.Attributes
{
    public class AlwaysAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}