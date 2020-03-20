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
        private readonly IWeather _Weather;

        public DataController(IWeather weather)
        {
            _Weather = weather;
        }

        [HttpGet("GetForecast")]
        public IActionResult GetForecast()
        {
            try
            {
                return Ok(_Weather.GetForecast());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}