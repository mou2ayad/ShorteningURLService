﻿using Microsoft.Extensions.DependencyInjection;
using App.Component.ShorteningURL.Contracts;
using App.Component.ShorteningURL.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using App.Component.Common.Models;
using App.Component.Common.Contracts;
using App.Component.ShortURLCache.Cache;
using Microsoft.Extensions.Caching.Memory;
using App.Component.ShortUrlStorage.SqlServer.DependencyInjection;
using App.Component.Utilities.DependencyInjection;

namespace App.Component.ShorteningURL.DependencyInjection
{
    public static class ShorteningUrlServiceInjectionExtension
    {
        public static void InjectShorteningUrlService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<ShorteningUrlConfigs>(configuration.GetSection("ShorteningUrlConfigs"));
            string CachingType= configuration.GetValue<string>("ShorteningUrlConfigs:CachingType");
            bool EnableCaching = configuration.GetValue<bool>("ShorteningUrlConfigs:EnableCaching");
            string StorageType = configuration.GetValue<string>("ShorteningUrlConfigs:StorageType");
            services.InjectRestAPIServices();
            if (EnableCaching)
            {
                switch (CachingType)
                {
                    case "MemoryCache":
                        {
                            services.TryAddSingleton<IMemoryCache, MemoryCache>();
                            services.AddSingleton<IShortURLCache, ShortURLMemoryCache>();                            
                            break;
                        }
                        
                    case "Redis":
                        //services.AddSingleton<IShortURLCache, ShortURLMemoryCache>(); // to add Redis cache 
                        break;
                    default: // memory cache in the default
                        services.AddSingleton<IShortURLCache, ShortURLMemoryCache>();
                        break;                        
                }
            }
            else
                services.AddSingleton<IShortURLCache, ShortURLNoCache>();

            switch (StorageType)
            {
                case "SqlServer":                    
                    services.InjectShortUrlStorageSqlServerServices();
                    break;
                case "DynamoDB":
                    //services.AddSingleton<IShortURLCache, ShorteningUrlDynamoDBStorageService>(); // just an example if we want to implement another type of storage
                    break;
                default:
                    throw new System.Exception("StorageType value in the Config is invalid or missing");                    
            }
            services.AddSingleton<IUrlFormatService, UrlFormatService>();
            services.AddSingleton<ILocalQueueService, LocalQueueService>();
            services.AddSingleton<IShorteningService, ShorteningService>();

        }
    }
}
