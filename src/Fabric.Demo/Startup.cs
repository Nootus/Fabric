//-------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This is application startup class. Application specific overrides go here
// </description>
//-------------------------------------------------------------------------------------------------
namespace Fabric.Demo
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Nootus.Fabric.Web;

    public class Startup : WebStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
            : base(configuration, env)
        {
        }
    }
}
