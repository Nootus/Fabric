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
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Nootus.Fabric.Web.Core;
    using Nootus.Fabric.Web.Core.Common;
    using Nootus.Fabric.Web.Core.Extensions;
    using Nootus.Fabric.Web.Core.Mapping;
    using Nootus.Fabric.Web.Security;
    using Nootus.Fabric.Web.Security.Extensions;
    using Nootus.Fabric.Web.Security.Filters;

    public class WebStartup
    {
        private List<IModuleStartup> modules = new List<IModuleStartup>
        {
            new SecurityStartup(),
        };

        public WebStartup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;

            // initializing all modules
            foreach (var module in this.modules)
            {
                module.Startup(this.Configuration);
            }

            SiteSettings.ConnectionString = configuration.GetConnectionString("WebApp");
            SiteSettings.EnvironmentName = env.EnvironmentName;
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
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(new NTAuthorizeFilter());
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
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession(new SessionOptions());
            app.UseContextMiddleware();

            // configuring services for all modules
            foreach (var module in this.modules)
            {
                module.Configure(app, env);
            }

            app.UseProfileMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "webapi",
                    template: "api/{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

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
    }
}
