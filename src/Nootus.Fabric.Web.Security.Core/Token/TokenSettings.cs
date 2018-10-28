namespace Nootus.Fabric.Web.Security.Core.Token
{
    public static class TokenSettings
    {
        public static string SymmetricKey { get; set; }
        public static string Issuer { get; set; }
        public static int LifeTime { get; set; }
        public static int MaxLifeTime { get; set; }
        public static int ClockSkew { get; set; }
    }
}
