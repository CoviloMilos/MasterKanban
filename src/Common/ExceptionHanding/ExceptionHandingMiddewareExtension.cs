using Microsoft.AspNetCore.Builder;

namespace Common.ExceptionHanding
{
    public static class ExceptionHandingMiddewareExtension
    {
        public static IApplicationBuilder UseRequestInvokerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandingMiddleware>();
        }
    }
}