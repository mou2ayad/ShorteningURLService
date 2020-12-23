
using Microsoft.Extensions.DependencyInjection;
using Nintex.NetCore.Component.Utilities.Async;

namespace Nintex.NetCore.Component.Utilities.DependencyInjection
{
    public static class AsyncActionQueueInjectionExtension
    {
        public static void InjectAsyncActionQueueServices(this IServiceCollection services) =>
            services.AddSingleton<IAsyncActionQueue, AsyncActionQueue>();           
        
    }
}
