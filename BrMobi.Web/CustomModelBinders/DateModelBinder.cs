using System;
using System.Web.Mvc;

namespace BrMobi.Web.CustomModelBinders
{
    public class DateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = controllerContext.HttpContext.Request.Form[bindingContext.ModelName];
            var dateValue = DateTime.Parse(value, new System.Globalization.CultureInfo("pt-BR"));

            return dateValue;
        }
    }
}