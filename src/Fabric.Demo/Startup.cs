using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nootus.Fabric.Web;

namespace Fabric.Demo
{
    public class Startup: WebStartup
    {
        public Startup(IConfiguration configuration): base(configuration)
        {
        }
    }
}
