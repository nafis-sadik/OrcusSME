using Repositories.Abstraction;
using System.Linq;
using DataLayer.Entities;

namespace Repositories.Implementation
{
    public class UserRepo : RepositoryBase<User>, IUserRepo { 
        public UserRepo() : base() { }

        public User FindUser(string userName, string pass) => Db.Users.FirstOrDefault(u => u.UserName == userName ||
                                                u.UserId == userName ||
                                                u.EmailIds.FirstOrDefault(m => m.EmailAddress == userName).UserId == u.UserId);
    }
}
