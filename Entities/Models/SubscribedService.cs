using DataLayer.Models;
using System;

namespace Models
{
    public class SubscribedService : BaseModel
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
