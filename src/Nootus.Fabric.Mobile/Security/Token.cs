using System;

namespace Nootus.Fabric.Mobile.Security
{
    public class Token
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime JwtTokenExpiry { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
