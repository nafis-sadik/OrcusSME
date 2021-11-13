using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UMS.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;

namespace WebApplication.Controllers
{
    [Route("api/Subscription")]
    [ApiController]
    public class SubscriptionController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        [Authorize]
        [Route("Subscribe/{userId}/{SubscriptionId}")]
        public IActionResult Subscribe(string UserId, int SubscriptionId)
        {
            if (_subscriptionService.Subscribe(UserId, SubscriptionId))
                return new OkObjectResult(new { Response = "" });
            else
                return new ConflictObjectResult(new { Response = CommonConstants.HttpResponseMessages.Exception });
        }

        [HttpGet]
        [Authorize]
        [Route("GetActiveSubscriptions/{userId}")]
        public IActionResult GetActiveSubscriptions(string userId) => new OkObjectResult(new { Response = _subscriptionService.GetActiveSubscriptions(userId) });

        [HttpGet]
        [Authorize]
        [Route("GetSubscriptionHistory/{pageNo}/{pageSize}/{userId}")]
        public IActionResult GetSubscriptionHistory(int pageNo, int? pageSize, string userId)
        {
            if (pageSize == null || pageSize <= 0)
                pageSize = CommonConstants.StandardPageSize;

            IEnumerable<SubscribedService> subHistory = _subscriptionService.GetSubscriptionHistory(new BaseModel
            {
                Page = pageNo,
                PageSize = (int)pageSize
            }, userId);

            return new OkObjectResult(new { Response = subHistory });
        }

        [HttpGet]
        [Authorize]
        [Route("HasSubscription/{userId}/{subscriptionId}")]
        public IActionResult HasSubscription(string userId, int subscriptionId) => new OkObjectResult(new { Response = _subscriptionService.HasSubscription(userId, subscriptionId) });
    }
}
