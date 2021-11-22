using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class SubscriptionLog
    {
        public string Subscription { get; set; }
        public string UserId { get; set; }
        public decimal? SubscriptionId { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual Service SubscriptionNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
