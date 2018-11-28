using System.Collections.Generic;

namespace Nootus.Fabric.Mobile.Security.Models
{
    public class UserProfileModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsRegistered { get; set; }
        public string FullName { get; set; }
    }
}
