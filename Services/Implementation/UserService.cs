using Repositories;
using Models;
using Services.Abstraction;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DataLayer;
using DataLayer.Entities;
using Repositories.Abstraction;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        private readonly IUserActivityLogRepo _userActivityLogRepo;
        private readonly IEmailIdRepo _emailIdRepo;

        public UserService(IUserRepo userRepo, ICrashLogRepo crashLogRepo, IUserActivityLogRepo userActivityLogRepo, IEmailIdRepo emailIdRepo)
        {
            _userRepo = userRepo;
            _crashLogRepo = crashLogRepo;
            _userActivityLogRepo = userActivityLogRepo;
            _emailIdRepo = emailIdRepo;
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

        public bool? LogIn(string userName, string pass, out string token, out string userId)
        {
            token = "";
            userId = "";
            User user = _userRepo.FindUser(userName, pass);
            if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(pass, user.Password))
            {
                userId = user.UserId;
                token = GenerateJwtToken(user.UserId);
                return true;
            }

            return false;
        }

        public bool? SignUp(UserModel user, out string token, out string userId)
        {
            token = "";
            userId = "";
            User existingUser = _userRepo.AsQueryable().FirstOrDefault(x => x.UserName == user.UserName);
            if (existingUser != null)
            {
                token = CommonConstants.HttpResponseMessages.UserNameExists;
                return null;
            }

            EmailId email = _emailIdRepo.AsQueryable().FirstOrDefault(x => x.EmailAddress == user.DefaultEmail);
            if (email != null)
            {
                token = CommonConstants.HttpResponseMessages.MailExists;
                return null;
            }

            try
            {
                userId = Guid.NewGuid().ToString();

                _userRepo.Add(new User
                {
                    UserId = userId,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Status = CommonConstants.StatusTypes.Active,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
                    UserName = user.UserName,
                    AccountBalance = CommonConstants.DefaultCreditBalance
                });

                int pk;
                if (_emailIdRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _emailIdRepo.AsQueryable().Max(x => x.Emailid) + 1;

                _emailIdRepo.Add(new EmailId {
                    Emailid = pk,
                    UserId = userId,
                    IsPrimaryMail = CommonConstants.True,
                    EmailAddress = user.DefaultEmail,
                    Status = CommonConstants.StatusTypes.Pending
                });

                token = GenerateJwtToken(userId);
                return true;
            }
            catch (Exception ex)
            {
                _userRepo.Rollback();

                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
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

        public bool ArchiveAccount(string userId)
        {
            try
            {
                User user = _userRepo.AsQueryable().FirstOrDefault(x => x.UserId == userId);
                if (user != null)
                {
                    user.Status = CommonConstants.StatusTypes.Archived;
                    _userRepo.Update(user);
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "UserService",
                    MethodName = "ArchiveAccount",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = userId,
                    TimeStamp = DateTime.Now
                });
                return false;
            }
        }

        public bool DeleteAccount(string userId)
        {

            try
            {
                User user = _userRepo.AsQueryable().FirstOrDefault(x => x.UserId == userId);
                if (user != null)
                {
                    user.Status = CommonConstants.StatusTypes.Archived;
                    _userRepo.Delete(user);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                int pk = _crashLogRepo.AsQueryable().Count() + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "UserService",
                    MethodName = "DeleteAccount",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = userId,
                    TimeStamp = DateTime.Now
                });
                return false;
            }
        }

        public bool ResetPassword(string userId)
        {
            try
            {
                User userData = _userRepo.AsQueryable().FirstOrDefault(x => x.UserId == userId);
                if (userData != null)
                {
                    userData.Password = BCrypt.Net.BCrypt.EnhancedHashPassword("ADIBA<3nafis");
                    _userRepo.Update(userData);
                    return true;
                }

                return false;
            } 
            catch (Exception ex)
            {
                int pk = _crashLogRepo.AsQueryable().Count() + 1;   

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "UserService",
                    MethodName = "ResetPassword",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = userId,
                    TimeStamp = DateTime.Now
                });
                return false;
            }
        }
    }
}
