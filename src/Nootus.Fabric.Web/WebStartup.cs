//-------------------------------------------------------------------------------------------------
// <copyright file="WebStartup.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Startup base classsed used by all applications
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Nootus.Fabric.Web.Core;
    using Nootus.Fabric.Web.Core.Common;
    using Nootus.Fabric.Web.Core.Extensions;
    using Nootus.Fabric.Web.Core.Mapping;
    using Nootus.Fabric.Web.Security.Core.Extensions;
    using Nootus.Fabric.Web.Security.Core.Filters;
    using Nootus.Fabric.Web.Security.SqlServer;
    using System.Collections.Generic;

    public class WebStartup
    {
        private readonly List<IMicroserviceStartup> modules = new List<IMicroserviceStartup>
        {
            // new SecuritySqlServerStartup(),
        };

        public WebStartup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;

            // initializing all modules
            FabricSettings.ConnectionString = configuration.GetConnectionString("WebApp");
            FabricSettings.EnvironmentName = env.EnvironmentName;
            FabricSettings.SessionClaims = configuration.GetSection("FabricSettings").GetValue<bool>("SessionClaims");
            FabricSettings.LoginDevEnvironment = configuration.GetSection("FabricSettings").GetValue<bool>("LoginDevEnvironment");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Automapper configurations
            Mapper.Initialize(x =>
            {
                x.AddProfile<CoreMappingProfile>();
                foreach (var module in this.modules)
                {
                    module.ConfigureMapping(x);
                }
            });

            // configuring services for all modules
            foreach (var module in this.modules)
            {
                module.ConfigureServices(services);
            }

            services.AddSession();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(new NTAuthorizeFilterAttribute());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseFileServer();
            app.UseSession(new SessionOptions());
            app.UseContextMiddleware();

            // configuring services for all modules
            foreach (var module in this.modules)
            {
                module.Configure(app);
            }

            app.UseProfileMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "webapi",
                    template: "api/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "ServiceStart" });

                routes.MapRoute(
                    name: "error",
                    template: "Error",
                    defaults: new { controller = "Home", action = "Error" });

                routes.MapRoute(
                    name: "catchall",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action}",
                   defaults: new { controller = "Home", action = "Index" });
            });
        }

        protected void Initialize(List<IMicroserviceStartup> appModules)
        {
            this.modules.AddRange(appModules);
            foreach (var module in this.modules)
            {
                module.Startup(this.Configuration);
            }
        }
    }
}
