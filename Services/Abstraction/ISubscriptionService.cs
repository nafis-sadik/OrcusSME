using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
    public interface ISubscriptionService
    {
        public bool Subscribe(int SubscriptionId);
        public bool GetSubscriptions(string UserId);
    }
}
