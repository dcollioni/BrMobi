using System;
using System.Web.Mvc;
using System.Globalization;

namespace BrMobi.Web.CustomModelBinders
{
    public class DateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = controllerContext.HttpContext.Request.Form[bindingContext.ModelName];
            
            DateTime dateValue;
            DateTime.TryParse(value, new System.Globalization.CultureInfo("pt-BR"), DateTimeStyles.None, out dateValue);

            DateTime? newValue = new DateTime?();

            if (dateValue != DateTime.MinValue)
            {
                newValue = dateValue;
            }

            return newValue;
        }
    }
}