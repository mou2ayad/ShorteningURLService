using Microsoft.Extensions.DependencyInjection;
using Nintex.NetCore.Component.Utilities.APIClient;

namespace Nintex.NetCore.Component.Utilities.DependencyInjection
{ 
    public static class RestAPIInjectionExtension
    {
        public static void InjectRestAPIServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IRestAPICaller, RestAPICaller>();
        }
            

    }
}
