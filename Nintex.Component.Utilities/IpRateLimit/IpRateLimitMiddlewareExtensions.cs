using Microsoft.AspNetCore.Builder;


namespace Nintex.NetCore.Component.Utilities.IpRateLimit
{ 
    public static class IpRateLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseNintexIpRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IpRateLimitMiddleware>();
        }
    }
}
