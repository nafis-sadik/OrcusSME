using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Route("api/[Subscription]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Subscribe(string UserId, int SubscriptionId)
        {
            if (_subscriptionService.Subscribe(UserId, SubscriptionId))
                return Ok();
            else
                return Conflict();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetActiveSubscriptions(string userId) => Ok(_subscriptionService.GetActiveSubscriptions(userId));

        [HttpGet]
        [Authorize]
        public IActionResult GetSubscriptionHistory(int pageNo, int? pageSize, string userId)
        {
            if (pageSize == null || pageSize <= 0)
                pageSize = CommonConstants.StandardPageSize;

            IEnumerable<SubscribedService> subHistory = _subscriptionService.GetSubscriptionHistory(new Pagination
            {
                PageNo = pageNo,
                PageSize = (int)pageSize
            }, userId);

            return Ok(subHistory);
        }

        [HttpGet]
        [Authorize]
        public IActionResult HasSubscription(string userId, int subscriptionId) => Ok(_subscriptionService.HasSubscription(userId, subscriptionId));
    }
}
