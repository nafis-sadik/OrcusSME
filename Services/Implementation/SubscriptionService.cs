using Entities;
using Entities.Models;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories;
using System.Linq;

namespace Services.Implementation
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionLogRepo _subscriptionLogRepo;
        private readonly ISubscriptionRepo _subscriptionRepo;
        private readonly ICrashLogRepo _crashLogRepo;
        public SubscriptionService(ISubscriptionLogRepo subscriptionLogRepo, ISubscriptionRepo subscriptionRepo, ICrashLogRepo crashLogRepo)
        {
            _subscriptionLogRepo = subscriptionLogRepo;
            _subscriptionRepo = subscriptionRepo;
            _crashLogRepo = crashLogRepo;
        }
        public IEnumerable<SubscribedService> GetActiveSubscriptions(string userId)
        {
            IQueryable<SubscriptionLog> SubscriptionLogs = _subscriptionLogRepo.AsQueryable().Where(x => x.SubscriptionDate > DateTime.Now);
            List<SubscribedService> subscriptions = new List<SubscribedService>();
            foreach(SubscriptionLog subscriptionHistory in SubscriptionLogs)
            {
                subscriptions.Add(new SubscribedService {
                    SubscriptionId = (int)subscriptionHistory.SubscriptionId,
                    ServiceName = subscriptionHistory.Subscription.SubscriptionName,
                    SubscriptionName = "Dana Shop"
                });
            }
            return subscriptions;
        }

        public IEnumerable<SubscribedService> GetSubscriptionHistory(Pagination pagination, string userId)
        {
            IQueryable<SubscriptionLog> SubscriptionLogs = _subscriptionLogRepo.AsQueryable().Where(x => x.UserId == userId).Skip(pagination.Skip).Take(pagination.PageSize);
            List<SubscribedService> subscriptions = new List<SubscribedService>();
            foreach (SubscriptionLog subscriptionHistory in SubscriptionLogs)
            {
                subscriptions.Add(new SubscribedService
                {
                    SubscriptionId = (int)subscriptionHistory.SubscriptionId,
                    ServiceName = subscriptionHistory.Subscription.SubscriptionName,
                    SubscriptionName = "Dana Shop",
                    ExpirationDate = subscriptionHistory.ExpirationDate,
                    SubscriptionDate = subscriptionHistory.SubscriptionDate,
                    SubscriptionPrice = (int)subscriptionHistory.Subscription.SubscriptionPrice
                });
            }
            return subscriptions;
        }

        public bool Subscribe(string UserId, int SubscriptionId, int ProductId)
        {
            try
            {
                IQueryable<SubscriptionLog> subscriptionLogs = _subscriptionLogRepo.AsQueryable().Where(x => x.UserId == UserId && x.SubscriptionId == SubscriptionId && x.ExpirationDate > DateTime.Today);
                Subscription subscriptions = _subscriptionRepo.Get(SubscriptionId);
                if (subscriptionLogs == null)
                    _subscriptionLogRepo.Add(new SubscriptionLog
                    {
                        SubscriptionId = SubscriptionId,
                        SubscriptionDate = DateTime.Now,
                        ExpirationDate = DateTime.Now.AddMonths((int)_subscriptionRepo.Get(SubscriptionId).DurationMonths),
                        UserId = UserId
                    });
                else
                {
                    foreach (SubscriptionLog subscription in subscriptionLogs)
                    {
                        subscription.ExpirationDate = DateTime.Now.AddMonths((int)_subscriptionRepo.Get(SubscriptionId).DurationMonths);
                        _subscriptionLogRepo.Update(subscription);
                    }
                }
                return true;
            } catch (Exception ex) {
                _crashLogRepo.Add(new CrashLog
                {
                    ClassName = "SubscriptionService",
                    Data = UserId.ToString() + " " + SubscriptionId.ToString() + " " + ProductId.ToString(),
                    ErrorInner = ex.InnerException != null ? ex.InnerException.Message : "",
                    ErrorMessage = ex.Message,
                    MethodName = "Subscribe",
                    TimeStamp = DateTime.Now
                });
                return false;
            }
        }
    }
}
