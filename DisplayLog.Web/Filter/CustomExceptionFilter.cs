using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Threading.Tasks;

namespace DisplayLog.Web.Filter
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception.GetBaseException();
            var errorMessage = exception.Message;
            context.ExceptionHandled = true;
            var request = (ControllerActionDescriptor)context.ActionDescriptor;
            Log.Error(exception, $"/{request.ControllerName}/{request.ActionName},Exception:{exception.ToString()}");
            return base.OnExceptionAsync(context);
        }
    }
}
