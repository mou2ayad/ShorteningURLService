using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Nintex.NetCore.Component.ShorteningURL.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nintex.NetCore.Component.Utilities.ErrorHandling;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Nintex.NetCore.Component.Utilities.IpRateLimit;
using Nintex.NetCore.Component.Utilities.DependencyInjection;

namespace Nintex.Services.ShorteningURLAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.AddHealthChecks();            
            services.InjectShorteningUrlService(Configuration);
            services.InjectAspNetCoreRateLimitService(Configuration);
            services.AddOptions();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nintex Shortening API",
                    Version = "v1",
                    Description = "Nintex Shortening API",
                    Contact = new OpenApiContact
                    {
                        Name = "Mouayad Khashfeh",
                        Email = "Mou2ayad@gmail.com",
                        Url = new Uri("https://github.com/mou2ayad"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> log)
        {
            app.ConfigurErrorHandler(log, "ShorteningURLAPI", env.IsDevelopment());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nintex Shortening API");            
                c.RoutePrefix = "ShorteningUrl";
            });
           
            app.UseHttpsRedirection();            
            app.UseRouting();
            app.UseHealthChecks(new PathString("/healthcheck"));
            app.UseNintexIpRateLimiting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });                    
          
        }
    }
}
