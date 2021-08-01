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

        public User FindUser(string userName, string pass)
        {
            User user = db.Users.FirstOrDefault(u => u.UserName == userName ||
                                                u.UserId == userName ||
                                                u.EmailIds.FirstOrDefault(m => m.EmailAddress == userName).UserId == u.UserId);

            int? pk = 0;
            if (db.UserActivityLogs.AsQueryable().Count() <= 0)
                pk = 0;
            else
                pk = db.UserActivityLogs.AsQueryable().Max(x => x.ActivityLogIn) + 1;

            db.UserActivityLogs.Add(new UserActivityLog
            {
                ActivityTypeId = pk,
                ActivityDate = DateTime.Now,
                ActivityLogIn = 'N',
                UserId = userName,
                ActivityType = db.ActivityTypes.Find(),
                Browser = "",
                Ipaddress = "",
                Misc = "",
                Os = "",
                Remarks = "",
                User = user
            });

            return user;
        }
    }
}
