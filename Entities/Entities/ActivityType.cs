using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            UserActivityLogs = new HashSet<UserActivityLog>();
        }

        public decimal ActivityTypeId { get; set; }
        public string ActivityName { get; set; }

        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }
    }
}
