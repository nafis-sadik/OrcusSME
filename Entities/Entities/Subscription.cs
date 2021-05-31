using System;
using System.Collections.Generic;

#nullable disable

namespace Entities
{
    public partial class Subscription
    {
        public decimal SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public decimal? SubscriptionPrice { get; set; }
        public decimal? DurationMonths { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
