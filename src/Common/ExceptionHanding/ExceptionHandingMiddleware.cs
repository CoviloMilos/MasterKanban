using System;
using System.Net;
using System.Threading.Tasks;
using Common.ExceptionHandling.Exceptions;
using Common.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ExceptionHanding
{
    public class ExceptionHandingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionHandingMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, BaseException ex) 
        {
            if (ex.code == 0) {
                ex.code = HttpStatusCode.InternalServerError;
            }

            var result = JsonConvert.SerializeObject(new { 
                messsage = ex.Message,
                status = ex.code,
                requested_uri = context.Request.Path,
                timestamp = DateTime.Now,
                origin = ex.origin
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) ex.code;

            return context.Response.WriteAsync(result);
        }
    }
}