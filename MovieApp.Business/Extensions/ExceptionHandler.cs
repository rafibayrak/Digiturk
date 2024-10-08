﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLog;
using System.Net;

namespace MovieApp.Business.Extensions
{
    public static class ExceptionHandler
    {
        public static void CustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { error = exception.Message };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error($"[UserName:{context.User.Identity.Name}] [RequestIp:{context.Connection.RemoteIpAddress}] [Path:{context.Request.Path}] [ErrorMessage:{exception.Message}]");
                await context.Response.WriteAsJsonAsync(response);
            }));
        }
    }
}
