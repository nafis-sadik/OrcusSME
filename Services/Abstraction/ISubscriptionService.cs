using Entities;
using Entities.Models;
using System.Collections.Generic;

namespace Services.Abstraction
{
    public interface ISubscriptionService
    {
        public bool Subscribe(string UserId, int Subscription, int ProductId);
        public IEnumerable<SubscribedService> GetActiveSubscriptions(string userId);
        public IEnumerable<SubscribedService> GetSubscriptionHistory(Pagination pagination, string userId);
    }
}
