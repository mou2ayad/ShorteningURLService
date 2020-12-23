using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nintex.NetCore.Component.Utilities.DependencyInjection;
using Nintex.Services.ShorteningURL.KeysGenerationService.Model;
using Nintex.Services.ShorteningURL.KeysGenerationService.Service;
using Nintex.NetCore.Component.Utilities.ErrorHandling;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Design;
using Nintex.Services.ShorteningURL.KeysGenerationService.DataAccess;

namespace Nintex.Services.ShorteningURL.KeysGenerationService
{
    public class Startup
    {
        public Startup(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public IConfiguration configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSingleton<IKGS, KGS>();
            services.AddSingleton<IKeysQueue, KeysQueue>();
            services.InjectAsyncActionQueueServices();
            services.AddOptions();
            services.AddSingleton<IDesignTimeDbContextFactory<KGSDbContext>, DBContextFactory>();
            services.AddSingleton<IKGSDBService, KGSDBService>();
            services.Configure<KGSConfig>(configuration.GetSection("KGS"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> log)
        {            
            app.ConfigurErrorHandler(log, "ShorteningURL.KeysGenerationService", env.IsDevelopment());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseHealthChecks(new PathString("/healthcheck"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
