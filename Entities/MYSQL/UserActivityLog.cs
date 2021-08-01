using System;
using System.Collections.Generic;

#nullable disable

namespace MySQL
{
    public partial class Useractivitylog
    {
        public string UserId { get; set; }
        public decimal? ActivityTypeId { get; set; }
        public string Remarks { get; set; }

        public virtual Activitytype ActivityType { get; set; }
        public virtual User User { get; set; }
    }
}
