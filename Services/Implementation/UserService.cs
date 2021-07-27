using Repositories;
using Models;
using Services.Abstraction;
using System;
using System.Linq;
using Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DataLayer;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        private readonly IUserActivityLogRepo _userActivityLogRepo;
        public UserService(IUserRepo userRepo, ICrashLogRepo crashLogRepo, IUserActivityLogRepo userActivityLogRepo)
        {
            _userRepo = userRepo;
            _crashLogRepo = crashLogRepo;
            _userActivityLogRepo = userActivityLogRepo;
        }

        private string GenerateJwtToken(string userId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] tokenKey = Encoding.ASCII.GetBytes(CommonConstants.PasswordConfig.Salt);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", userId)
                }),
                Expires = DateTime.UtcNow.AddDays(CommonConstants.PasswordConfig.SaltExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool? LogIn(string userName, string pass, out string token)
        {
            throw new NotImplementedException();
        }

        public bool SignUp(UserModel user, out string token)
        {
            token = "";
            try
            {
                _userRepo.Add(new User
                {
                    UserId = Guid.NewGuid().ToString(),
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Status = CommonConstants.StatusTypes.Active,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
                    UserName = user.UserName
                });
                token = GenerateJwtToken(user.UserId);
                return true;
            }
            catch (Exception ex)
            {
                _crashLogRepo.Add(new Crashlog
                {
                    ClassName = "UserService",
                    MethodName = "SignUp",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = user.ToString(),
                    TimeStamp = DateTime.Now
                });
                return false;
            }
        }
    }
}
