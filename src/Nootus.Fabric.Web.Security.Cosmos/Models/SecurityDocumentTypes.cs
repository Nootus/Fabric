using System;
using System.Collections.Generic;
using System.Text;

namespace Nootus.Fabric.Web.Security.Cosmos.Models
{
    public class SecurityDocumentTypes
    {
        public string UserProfile { get; set; }
        public string UserAuth { get; set; }
        public string Role { get; set; }
        public string Claim { get; set; }
        public string Page { get; set; }
        public string AndroidSettings { get; set; }
    }
}
