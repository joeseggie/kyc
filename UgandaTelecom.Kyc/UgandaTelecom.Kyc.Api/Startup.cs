using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Services;
using UgandaTelecom.Kyc.Core.Services.Msente;
using UgandaTelecom.Kyc.Core.Services.Subscribers;

namespace UgandaTelecom.Kyc.Api
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
            // Getting the application settings
            services.Configure<ConnectionStringsAppSettings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<MsenteAppSettings>(Configuration.GetSection("Msente"));

            services.AddTransient<ISqlDatabaseServer, SqlDatabaseServer>();
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddTransient<IMsenteService, MsenteService>();
            services.AddTransient<IUrlHandlerService, UrlHandlerService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
