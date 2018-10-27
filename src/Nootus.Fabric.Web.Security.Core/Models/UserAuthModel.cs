namespace Nootus.Fabric.Web.Security.Core.Models
{
    public class UserAuthModel
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
    }
}
