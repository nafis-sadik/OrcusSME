using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class SubscribedService
    {
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string ServiceName { get; set; }
        public int SubscriptionPrice { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public decimal? DurationMonths { get; set; }
    }
}
