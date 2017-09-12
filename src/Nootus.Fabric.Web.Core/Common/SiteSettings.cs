//-------------------------------------------------------------------------------------------------
// <copyright file="SiteSettings.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  This contains the site settings defined in the appSettings.json. It also contains environment
//  variables
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.Common
{
    using System;

    public static class SiteSettings
    {
        public static string ConnectionString { get; set; }

        public static string WebPath { get; set; }

        public static string EnvironmentName { get; set; }

        public static bool IsEnvironment(string environmentName)
        {
            return string.Equals(environmentName, EnvironmentName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
