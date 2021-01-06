
using App.Component.Common.Contracts;
using App.Component.ShorteningURL.Contracts;
using System;
using Microsoft.Extensions.DependencyInjection;
using App.Component.ShorteningURL.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Testing.Component.ShorteningUrlTest
{
    internal static class ServiceProviderFactory
    {
        private static ServiceProvider serviceProvider;
        public static ServiceProvider GetServiceProvider() {
            if (serviceProvider == null)
                serviceProvider = ServiceProvider.GetInstance();
            return serviceProvider;
        }
    }
    internal class ServiceProvider
    {
        private IConfiguration configuration { get; set; }
        private IServiceProvider _serviceProvider { set; get; }
        private ServiceProvider()
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            configuration = config;
            _serviceProvider = GetServiceProvider();
        }
        public static ServiceProvider GetInstance()
        {
            return new ServiceProvider();
        }
        private IServiceProvider GetServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddOptions();
            services.InjectShorteningUrlService(configuration);
            return services.BuildServiceProvider();
        }

        public IUrlFormatService GetUrlFormatService()
        {                        
            return _serviceProvider.GetService(typeof(IUrlFormatService)) as IUrlFormatService;
        }
        public IShortURLStorage GetShortURLStorageService()
        {            
            return _serviceProvider.GetService(typeof(IShortURLStorage)) as IShortURLStorage;
        }
        public  IShortURLCache GetShortURLCacheService()
        {                      
            return _serviceProvider.GetService(typeof(IShortURLCache)) as IShortURLCache;
        }
        public IShorteningService GetShorteningService()
        {
            return _serviceProvider.GetService(typeof(IShorteningService)) as IShorteningService;
        }

    }
}
