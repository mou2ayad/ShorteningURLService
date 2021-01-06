using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using App.Component.ShortUrlStorage.SqlServer.Persistence;
using App.Component.Common.Models;

namespace App.Component.ShortUrlStorage.SqlServer
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


