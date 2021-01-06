
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using App.Component.Common.Contracts;
using App.Component.ShortUrlStorage.SqlServer.Persistence;
using App.Component.ShortUrlStorage.SqlServer.Service;

namespace App.Component.ShortUrlStorage.SqlServer.DependencyInjection
{
    public static class ShortUrlStorageSqlServerInjectionExtension
    {
        public static void InjectShortUrlStorageSqlServerServices(this IServiceCollection services)
        {
            services.AddSingleton<IDesignTimeDbContextFactory<UrlShorteningDbContext>, DBContextFactory>();
            services.AddSingleton<IShortURLStorage, ShorteningUrlSqlServerStorageService>();

        }


    }

}
