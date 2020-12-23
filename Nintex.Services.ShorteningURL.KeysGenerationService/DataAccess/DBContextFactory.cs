using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nintex.Services.ShorteningURL.KeysGenerationService.Model;

namespace Nintex.Services.ShorteningURL.KeysGenerationService.DataAccess
{
    public class DBContextFactory : IDesignTimeDbContextFactory<KGSDbContext>
    {
        private readonly string ConnectionString;
        public DBContextFactory(IOptions<KGSConfig> options)
        {
            ConnectionString = options.Value.DBConnectionString;
        }


        KGSDbContext IDesignTimeDbContextFactory<KGSDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KGSDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new KGSDbContext(optionsBuilder.Options);
        }
    }
}


