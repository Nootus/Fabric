﻿namespace Nootus.Fabric.Web.Security.Models
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
