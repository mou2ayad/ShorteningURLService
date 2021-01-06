
using Microsoft.Extensions.DependencyInjection;
using App.Component.Utilities.Async;

namespace App.Component.Utilities.DependencyInjection
{
    public static class AsyncActionQueueInjectionExtension
    {
        public static void InjectAsyncActionQueueServices(this IServiceCollection services) =>
            services.AddSingleton<IAsyncActionQueue, AsyncActionQueue>();           
        
    }
}
