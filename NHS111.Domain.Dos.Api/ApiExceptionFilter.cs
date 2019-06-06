using System;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace NHS111.Domain.Dos.Api
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var controllerAction = string.Empty;
            if (context.RouteData != null && context.RouteData != null & context.RouteData.Values != null)
            {
                controllerAction = string.Join("/", context.RouteData.Values.Values);
            }

            var data = "";
            foreach (DictionaryEntry pair in context.Exception.Data)
            {
                data += $"{pair.Key}: {pair.Value}{Environment.NewLine}";
            }

            var errorMsg = $"{context.Exception.GetType().FullName} occured executing '{controllerAction}'{Environment.NewLine}{context.Exception.Message}{Environment.NewLine}{context.Exception.StackTrace}{Environment.NewLine}Exception.Data:{Environment.NewLine}{data}";
            _logger.LogError(errorMsg);
        }
    }
}
