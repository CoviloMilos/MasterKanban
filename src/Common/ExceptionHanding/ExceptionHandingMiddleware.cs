using System.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using Common.DistribudetExcetionHandling;
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
        private readonly IRabbitMqManager _manager;

        public ExceptionHandingMiddleware(RequestDelegate next, ILoggerManager logger, IRabbitMqManager manager)
        {
            _logger = logger;
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                try
                {
                    await handleCustomExceptions(ex as BaseException, httpContext);
                }
                catch (Exception)
                {
                    await handleAll(ex, httpContext);
                }
            }
        }

        private async Task handleCustomExceptions(BaseException ex, HttpContext httpContext) 
        {
            if (ex.code == 0) {
                    ex.code = HttpStatusCode.InternalServerError;
                }

            var response = JsonConvert.SerializeObject(new { 
                messsage = ex.Message,
                status = ex.code,
                requested_uri = httpContext.Request.Path,
                timestamp = DateTime.Now,
                origin = ex.origin
            });

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) ex.code;

            await httpContext.Response.WriteAsync(response);

            if (ex.code.Equals(HttpStatusCode.InternalServerError))
                _manager.Publish(response);
        }

        private async Task handleAll(Exception ex, HttpContext httpContext) 
        {
            var response = JsonConvert.SerializeObject(new { 
                messsage = ex.Message,
                status = HttpStatusCode.InternalServerError,
                requested_uri = httpContext.Request.Path,
                timestamp = DateTime.Now,
                origin = getOriginBasedOnContext(httpContext.Request.Path)
            });

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsync(response);

            _manager.Publish(response);
        }

        private string getOriginBasedOnContext(PathString path)
        {
            if (path.ToString().Contains("project") || path.ToString().Contains("assignment"))
                return "KanbanManagement.API";

            return null;
        }
    }
}