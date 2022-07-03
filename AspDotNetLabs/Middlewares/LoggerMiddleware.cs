using Microsoft.AspNetCore.Http.Features;

namespace AspDotNetLabs.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;

        public LoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerFactory loggerFactory)
        {
            var feature = httpContext.Features.Get<IHttpConnectionFeature>();
            var ip = feature?.LocalIpAddress?.ToString();
            string message = $"time: {DateTime.Now}, ip: {ip} {httpContext.Request.Path}";
            var logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation(message);
            await next.Invoke(httpContext);
        }
    }
}
