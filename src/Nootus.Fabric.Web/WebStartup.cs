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
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nootus.Fabric.Web.Core;
    using Nootus.Fabric.Web.Security;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WebStartup
    {
        public IConfiguration Configuration { get; }
        protected List<IModuleStartup> components = new List<IModuleStartup>
        {
            new SecurityStartup()
        };


        public WebStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
