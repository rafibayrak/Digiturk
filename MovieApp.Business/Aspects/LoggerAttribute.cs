using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using NLog;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MovieApp.Business.Aspects
{
    public class LoggerAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stream = context.HttpContext.Request.Body; 
            context.HttpContext.Request.EnableBuffering();
            context.HttpContext.Request.Body.Position = 0;
            var requestBody = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                requestBody = await reader.ReadToEndAsync();
                context.HttpContext.Request.Body.Position = 0;
            }

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info($"[RequestIp:{context.HttpContext.Connection.RemoteIpAddress}] [Path:{context.HttpContext.Request.Path}] [RequestBody{requestBody}]");
            await next();
        }
    }
}
