using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleNetBackend
{
    [ApiController]
    [Route("/api")]
    public class ApiController : Controller
    {
        [HttpGet("ping")]
        public IActionResult GetPing()
        {
            return new JsonResult(new {
                status = "ok"
            });
        }
    }
}
