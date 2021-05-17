using Repositories;
using Entities.Models;
using Services.Abstraction;
using System;
using System.Linq;
using Entities;
using DevOne.Security.Cryptography.BCrypt;

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

        public bool? LogIn(string userId, string password)
        {
            User user = _userRepo.AsQueryable().FirstOrDefault(x => x.UserName == userId || x.UserId == userId);
            if (user == null)
                return null;

            if (BCryptHelper.CheckPassword(password, user.Password))
                return true;
            else
                return false;
        }

        public bool SignUp(UserModel _user)
        {
            User user = new User
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = _user.FirstName,
                MiddleName = _user.MiddleName,
                LastName = _user.LastName,
                Status = CommonConstants.StatusTypes.Active,
                Password = BCryptHelper.HashPassword(_user.Password, BCryptHelper.GenerateSalt(CommonConstants.PasswordConfig.SaltGeneratorLogRounds)),
                UserName = _user.UserName
            };
            try
            {
                _userRepo.Add(user);
            }
            catch (Exception ex)
            {
                _crashLogRepo.Add(new CrashLog
                {
                    ClassName = "UserService",
                    MethodName = "SignUp",
                    ErrorMessage = ex.Message,
                    ErrorInner = ex.InnerException.Message,
                    Data = user.ToString(),
                    TimeStamp = DateTime.Now
                });
                return false;
            }
            return true;
        }
    }
}
