using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusDisplayApi.Interfaces;

namespace StatusDisplayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IVK _VK;

        public DataController(IVK VK)
        {
            _VK = VK;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(_VK.test().ToString());
        }
    }
}