﻿using Microsoft.AspNetCore.Identity;
using Nootus.Fabric.Web.Core.Context;
using Nootus.Fabric.Web.Core.Cosmos.Models;
using Nootus.Fabric.Web.Core.Exception;
using Nootus.Fabric.Web.Core.Notification;
using Nootus.Fabric.Web.Security.Core.Common;
using Nootus.Fabric.Web.Security.Core.Domain;
using Nootus.Fabric.Web.Security.Core.Models;
using Nootus.Fabric.Web.Security.Core.Services;
using Nootus.Fabric.Web.Security.Core.Token;
using Nootus.Fabric.Web.Security.Cosmos.Middleware;
using Nootus.Fabric.Web.Security.Cosmos.Models;
using Nootus.Fabric.Web.Security.Cosmos.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nootus.Fabric.Web.Security.Cosmos.Domain
{
    public class AccountService : BaseService<SecurityDbContext>, IAccountService
    {
        public Task ChangePassword(ChangePasswordModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task Logout()
            => // there is no logout for JWT Tokens
            await Task.CompletedTask;

        public async Task<UserProfileModel> ProfileGet()
        {
            if (NTContext.Context.UserName == null)
                return null;

            return await ProfileGet(NTContext.Context.UserName);
        }

        public async Task<UserProfileModel> ProfileGet(string username)
            => await DbService.GetModelByKeyAsyc<UserProfileModel>(username, SecurityAppSettings.ServiceSettings.DocumentTypes.UserProfile);

        public async Task<UserProfileModel> Register(RegisterUserModel model)
        {
            SharedCollectionDocument<UserProfileModel> profileDocument = await DbService.GetDocumentByKeyAsyc<UserProfileModel>(model.UserName, SecurityAppSettings.ServiceSettings.DocumentTypes.UserProfile);
            UserProfileModel profileModel = profileDocument.Model;
            profileModel.FirstName = model.FirstName;
            profileModel.LastName = model.LastName;
            profileModel.IsRegistered = true;

            await DbService.ReplaceDocumentAsync(profileDocument);

            return profileModel;
        }

        public async Task SendOtp(string mobileNumber)
        {
            int otp = 123456;
            //int otp = new Random().Next(100000, 999999); // random OTP

            SharedCollectionDocument<UserAuthModel> userAuthDocument = await DbService.GetDocumentByKeyAsyc<UserAuthModel>(mobileNumber, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);

            if(userAuthDocument == null)
            {
                UserAuthModel user = new UserAuthModel()
                {
                    UserName = mobileNumber,
                    PasswordHash = PasswordService.DefaultPassword,
                    Otp = otp,
                    RefreshToken = TokenService.GenerateRefreshToken(),
                };

                SharedCollectionDocument<UserAuthModel> document = await DbService.CreateDocumentAsync(user.UserName, user, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);
            }
            else
            {
                userAuthDocument.Model.Otp = otp;
                await DbService.ReplaceDocumentAsync(userAuthDocument);
            }

            string appHash = CacheService.AndroidSignatureHash;
            string message = String.Format(SmsManager.OtpTemplate, otp, appHash);

            SmsManager.SendSms(mobileNumber, message);
        }

        public async Task<UserProfileModel> ValidateOtp(LoginModel login)
        {
            SharedCollectionDocument<UserAuthModel> userAuthDocument = await DbService.GetDocumentByKeyAsyc<UserAuthModel>(login.UserName, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);
            if (userAuthDocument == null)
            {
                throw new NTException(SecurityMessages.InvalidMobileNumber);
            }

            if(userAuthDocument.Model.Otp != login.Otp)
            {
                throw new NTException(SecurityMessages.InvalidOtp);
            }

            UserProfileModel userProfile = await ProfileGet(userAuthDocument.Model.UserName);
            if(userProfile == null)
            {
                userProfile = new UserProfileModel()
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = userAuthDocument.Model.UserName,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    IsRegistered = false,
                    CompanyId = 0,
                    Roles = new List<RoleModel>()
                    {
                        new RoleModel() { Name = "Player" }
                    }
                };
                await DbService.CreateDocumentAsync(userProfile.UserName, userProfile, SecurityAppSettings.ServiceSettings.DocumentTypes.UserProfile);
            }
            return await CreateToken(userProfile, userAuthDocument);
        }

        public async Task<UserProfileModel> Validate(LoginModel login)
        {
            // check user name and password from database
            SharedCollectionDocument<UserAuthModel> userAuthDocument = await DbService.GetDocumentByKeyAsyc<UserAuthModel>(login.UserName, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);
            if(userAuthDocument == null)
            {
                throw new NTException(SecurityMessages.InvalidUsernamePassword);
            }

            bool result = PasswordService.VerifyHashedPassword(userAuthDocument.Model.PasswordHash, login.UserPassword);
            if (!result)
            {
                throw new NTException(SecurityMessages.InvalidUsernamePassword);
            }

            return await CreateToken(login.UserName, userAuthDocument);

        }

        private async Task<UserProfileModel> CreateToken(string userName, SharedCollectionDocument<UserAuthModel> userAuthDocument)
        {
            UserProfileModel userProfile = await ProfileGet(userName);
            return await CreateToken(userProfile, userAuthDocument);
        }

        private async Task<UserProfileModel> CreateToken(UserProfileModel userProfile, SharedCollectionDocument<UserAuthModel> userAuthDocument)
        {
            // creating tokens
            string jwtToken = TokenService.GenerateJwtToken(userProfile);

            // if no refresh token exists, then create one
            if (string.IsNullOrEmpty(userAuthDocument.Model.RefreshToken))
            {
                userAuthDocument.Model.RefreshToken = TokenService.GenerateRefreshToken();
                await DbService.ReplaceDocumentAsync(userAuthDocument);
            }

            TokenService.SetTokenHeader(jwtToken, userAuthDocument.Model.RefreshToken);
            return userProfile;
        }

        public async Task RefreshToken(string jwtToken, string refreshToken)
        {
            // getting user name from token
            ClaimsPrincipal principal = TokenService.GetPrincipalFromToken(jwtToken);
            if(principal != null)
            {
                NTContext.Context.UserName = principal.Identity.Name;
                UserAuthModel authModel = await DbService.GetModelByKeyAsyc<UserAuthModel>(principal.Identity.Name, SecurityAppSettings.ServiceSettings.DocumentTypes.UserAuth);

                // checking refresh token validity
                if (authModel.RefreshToken == refreshToken)
                {
                    TokenService.GenerateJwtToken(principal);
                }
                else
                {
                    TokenService.SetTokenHeader(refreshTokenExpired: true);
                }
            }
            else
            {
                TokenService.SetTokenHeader(refreshTokenExpired: true);
            }
        }
    }
}
