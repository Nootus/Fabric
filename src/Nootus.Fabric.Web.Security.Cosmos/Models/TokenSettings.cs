using System;
using System.Collections.Generic;
using System.Text;

namespace Nootus.Fabric.Web.Security.Cosmos.Models
{
    public static class TokenSettings
    {
        public static string SymmetricKey { get; set; }
        public static string Issuer { get; set; }
        public static int Duration { get; set; }
    }
}
