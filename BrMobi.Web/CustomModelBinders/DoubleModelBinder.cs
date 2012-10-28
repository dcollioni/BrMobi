using System.Web.Mvc;

namespace BrMobi.Web.CustomModelBinders
{
    public class DoubleModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = controllerContext.HttpContext.Request.Form[bindingContext.ModelName];
            var doubleValue = double.Parse(value, new System.Globalization.CultureInfo("en-US"));

            return doubleValue;
        }
    }
}