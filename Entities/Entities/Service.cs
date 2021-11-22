using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class Service
    {
        public decimal SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public decimal? SubscriptionPrice { get; set; }
        public decimal? DurationMonths { get; set; }
    }
}
