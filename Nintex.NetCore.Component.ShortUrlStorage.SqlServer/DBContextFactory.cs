using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Persistence;
using Nintex.NetCore.Component.Common.Models;

namespace Nintex.NetCore.Component.ShortUrlStorage.SqlServer
{
    public class DBContextFactory : IDesignTimeDbContextFactory<UrlShorteningDbContext>
    {
        private readonly string ConnectionString;
        public DBContextFactory(IOptions<ShorteningUrlConfigs> options)
        {
            ConnectionString = options.Value.SqlServerStorageConnectionString;
        }
        

        UrlShorteningDbContext IDesignTimeDbContextFactory<UrlShorteningDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UrlShorteningDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new UrlShorteningDbContext(optionsBuilder.Options);
        }
    }
}


