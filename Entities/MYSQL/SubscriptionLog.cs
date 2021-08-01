using System;
using System.Collections.Generic;

#nullable disable

namespace MySQL
{
    public partial class Subscriptionlog
    {
        public string UserId { get; set; }
        public decimal? SubscriptionId { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public virtual Subscription Subscription { get; set; }
        public virtual User User { get; set; }
    }
}
