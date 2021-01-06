using Microsoft.AspNetCore.Builder;


namespace App.Component.Utilities.IpRateLimit
{ 
    public static class IpRateLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseAppIpRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpRateLimitMiddleware>();
        }
    }
}
