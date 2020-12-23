
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Nintex.NetCore.Component.Common.Contracts;
using Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Persistence;
using Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Service;

namespace Nintex.NetCore.Component.ShortUrlStorage.SqlServer.DependencyInjection
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
