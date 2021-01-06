using Microsoft.Extensions.DependencyInjection;
using App.Component.Utilities.APIClient;

namespace App.Component.Utilities.DependencyInjection
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
