using Nootus.Fabric.Mobile.Security;
using Nootus.Fabric.Mobile.Security.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Nootus.Fabric.Mobile.Settings
{
    public class Session
    {
        public Assembly ResourceAssembly { get; set; }
        public Token Token { get; set; }
        public UserProfileModel UserProfile { get; set; } 

        public bool IsAuthenticated
        {
            get
            {
                if (Token.RefreshToken != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
