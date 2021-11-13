using DataLayer.Models;

namespace Models
{
    public class UserModel : BaseModel
    {
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
