using DataLayer;
using Entities;
using Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class UserRepo : RepositoryBase<User>, IUserRepo { 
        public UserRepo() : base() { }

        public User FindUser(string userName, string pass) => db.Users.FirstOrDefault(u => u.UserName == userName ||
                                                u.UserId == userName ||
                                                u.EmailIds.FirstOrDefault(m => m.EmailAddress == userName).UserId == u.UserId);
    }
}
