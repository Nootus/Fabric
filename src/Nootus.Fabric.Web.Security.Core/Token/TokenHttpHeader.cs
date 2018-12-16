namespace Nootus.Fabric.Web.Security.Core.Token
{
    public class TokenHttpHeader
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public int JwtLifeTime { get; set; }
        public int MaxLifeTime { get; set; }
        public bool JwtTokenExpired { get; set; }
        public bool RefreshTokenExpired { get; set; }
    }
}
