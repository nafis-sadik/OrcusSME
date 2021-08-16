using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/Outlet")]
    [ApiController]
    public class OutletController
    {
        private readonly IOutletManagerService _outletManagerService;
        public OutletController(IOutletManagerService outletManagerService)
        {
            _outletManagerService = outletManagerService;
        }

        [Authorize]
        [HttpPut]
        [Route("Add")]
        public IActionResult Add(Outlet outlet)
        {
            List<Outlet> data = _outletManagerService.AddOutlet(outlet);
            if (data != null && data.Count > 0)
                return new OkObjectResult(new { Response = data });
            else if(data == null)
                return new ConflictObjectResult(new { Response = "Store Already Exists" });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpPost]
        [Route("Archive")]
        public IActionResult Archive(Outlet outlet)
        {
            List<Outlet> data = _outletManagerService.ArchiveOutlet(outlet);
            if (data.Count > 0)
                return new OkObjectResult(new { Response = data });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Outlet outlet)
        {
            List<Outlet> data = _outletManagerService.UpdateOutlet(outlet);
            if (data.Count > 0)
                return new OkObjectResult(new { Response = data });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpGet]
        [Route("GetUserOutlets/{userId}")]
        public IActionResult GetOutletsByUserId(string userId)
        {
            List<Outlet> data = _outletManagerService.GetOutletsByUserId(userId);
            if (data.Count > 0)
                return new OkObjectResult( new { Response = data } );
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpGet]
        [Route("GetOutlet/{OutletId}")]
        public IActionResult GetOutlet(decimal OutletId)
        {
            Outlet data = _outletManagerService.GetOutlet(OutletId);
            if (data != null)
                return new OkObjectResult(new { Response = data });
            else
                return new ConflictObjectResult(new { Response = "Error" });
        }

        [Authorize]
        [HttpGet]
        [Route("OrderSite/{OutletId}")]
        public IActionResult OrderSite(decimal OutletId)
        {
            bool? responseType = _outletManagerService.OrderSite(OutletId, out string response);
            if (responseType == true)
                return new OkObjectResult(new { Response = response });
            else if(responseType == false)
                return new OkObjectResult(new { Response = response });
            else
                return new ConflictObjectResult(new { Response = response });
        }
    }
}
