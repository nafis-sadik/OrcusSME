using Entities.Models;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface ISubscriptionService
    {
        public bool Subscribe(string UserId, int SubscriptionId);
        public IEnumerable<SubscribedService> GetActiveSubscriptions(string userId);
        public IEnumerable<SubscribedService> GetSubscriptionHistory(Pagination pagination, string userId);
        public bool HasSubscription(string userId, int subscriptionId);
    }
}
