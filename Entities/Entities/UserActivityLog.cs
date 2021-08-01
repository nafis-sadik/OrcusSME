using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class UserActivityLog
    {
        public int ActivityLogIn { get; set; }
        public string UserId { get; set; }
        public decimal? ActivityTypeId { get; set; }
        public string Remarks { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string Ipaddress { get; set; }
        public string Browser { get; set; }
        public string Os { get; set; }
        public string Misc { get; set; }

        public virtual ActivityType ActivityType { get; set; }
        public virtual User User { get; set; }
    }
}
