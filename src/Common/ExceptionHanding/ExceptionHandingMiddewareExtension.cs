using Microsoft.AspNetCore.Builder;

namespace Common.ExceptionHanding
{
    public static class ExceptionHandingMiddewareExtension
    {
        public static IApplicationBuilder UseExcetionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandingMiddleware>();
        }
    }
}