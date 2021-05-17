using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface IUserService
    {
        public bool SignUp(UserModel user);
        public bool? LogIn(string userId, string password);
    }
}
