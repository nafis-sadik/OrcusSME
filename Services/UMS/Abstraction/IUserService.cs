using Models;

namespace UMS.Services.Abstraction
{
    public interface IUserService
    {
        public bool? SignUp(UserModel userModel, out string token, out string userId);
        public bool? LogIn(UserModel userModel, out string token, out string userId);
        public bool ArchiveAccount(string userId);
        public bool DeleteAccount(string userId);
        public bool ResetPassword(string userId);
    }
}
