using System;

namespace Nootus.Fabric.Mobile.Security
{
    public class Token
    {
        public string JwtToken { get; set; }
        public DateTime RefreshTokenExpiryDate { get; set; } 
        public string RefreshToken { get; set; }
    }
}
