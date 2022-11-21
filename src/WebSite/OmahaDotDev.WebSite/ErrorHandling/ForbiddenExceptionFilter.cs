using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OmahaDotDev.Model.Common.Exceptions;
using System.Net;

namespace OmahaDotDev.WebSite.ErrorHandling
{
    public class ForbiddenExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ForbiddenExceptionFilter(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is ForbiddenException e)
            {
                context.Result = new JsonResult(e.Message)
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    Value = _hostEnvironment.IsDevelopment() ? e.Message : "Forbidden"
                };
            }


        }
    }


}
