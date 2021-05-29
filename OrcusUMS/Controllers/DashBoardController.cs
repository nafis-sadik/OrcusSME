using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index() => View();
        [Authorize]
        public IActionResult GetSubscriptions()
        {
            throw new NotImplementedException();
        }
    }
}
