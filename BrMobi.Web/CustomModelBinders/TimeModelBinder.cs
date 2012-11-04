using System;
using System.Web.Mvc;

namespace BrMobi.Web.CustomModelBinders
{
    public class TimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = controllerContext.HttpContext.Request.Form[bindingContext.ModelName];
            var timeValue = TimeSpan.Parse(value, new System.Globalization.CultureInfo("pt-BR"));

            return timeValue;
        }
    }
}