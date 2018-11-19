//-------------------------------------------------------------------------------------------------
// <copyright file="SecurityMessages.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  User messages in this project
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Security.Core.Common
{
    public static class SecurityMessages
    {
        public const string LoginSuccess = "Successfully logged in";
        public const string LogoutSuccess = "Successfully logged out";

        public const string PasswordsDifferent = "Password and confirm passwords should be same";
        public const string RegisterUserError = "Unable to register user";
        public const string RegisterUserSuccess = "Successfully registered";

        public const string InvalidUsernamePassword = "Invalid Username and/or Password";
        public const string ChangePasswordSuccess = "Password Changed Successfully";
        public const string ChangePasswordError = "Unable to change password";
        public const string InvalidMobileNumber = "Mobile number does not exist";
        public const string InvalidOtp = "Invalid OTP";


        public const string InvalidToken = "Invalid Token";
    }
}
