using Repositories.Abstraction;
using System.Linq;
using DataLayer.Entities;
using DataLayer.MSSQL;

namespace Repositories.Implementation
{
    public class UserRepo : RepositoryBase<User>, IUserRepo
    {
        public UserRepo(OrcusSMEContext context) : base(context) { }

        public User FindUser(string userName, string pass)
        {
            User user = Db.Users.FirstOrDefault(u =>
                u.UserName.Equals(userName) ||
                u.UserId.Equals(userName));
            //if (user == null)
            //    user = Db.EmailAddresses.FirstOrDefault(u => u.EmailAddress1.Equals(userName)).User;
            
            return user;
        }
    }
}
