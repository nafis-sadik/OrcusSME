using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class UserActivityLog
    {
        public string UserId { get; set; }
        public decimal? ActivityTypeId { get; set; }
        public string Remarks { get; set; }
        public DateTime? ActivityDate { get; set; }

        public virtual ActivityType ActivityType { get; set; }
        public virtual User User { get; set; }
    }
}
