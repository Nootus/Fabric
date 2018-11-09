namespace Nootus.Fabric.Mobile.Core
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthenticated { get; set; } = false;
    }
}
