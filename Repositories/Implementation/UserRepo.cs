using Repositories.Abstraction;
using System.Linq;
using DataLayer.Entities;

namespace Repositories.Implementation
{
    public class UserRepo : RepositoryBase<User>, IUserRepo { 
        public UserRepo() : base() { }

        public User FindUser(string userName, string pass) => Db.Users.FirstOrDefault(u => u.UserName == userName ||
                                                u.UserId == userName ||
                                                u.EmailAddresses.FirstOrDefault(m => m.EmailAddress1 == userName).UserId == u.UserId);
    }
}
