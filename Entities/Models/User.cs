using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicLoc { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }
        public decimal? AccountBalance { get; set; }
        public string DefaultEmail { get; set; }
    }
}
