using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class User
    {
        public User()
        {
            EmailIds = new HashSet<EmailId>();
            Outlets = new HashSet<Outlet>();
            UserActivityLogs = new HashSet<UserActivityLog>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicLoc { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public decimal? AccountBalance { get; set; }

        public virtual ICollection<EmailId> EmailIds { get; set; }
        public virtual ICollection<Outlet> Outlets { get; set; }
        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }
    }
}
