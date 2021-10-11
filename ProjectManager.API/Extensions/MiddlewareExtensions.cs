using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ProjectManager.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionsHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(c => c.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new { Message = "Internal server error" });
                await context.Response.WriteAsync(result);
            }));
        }
    }
}