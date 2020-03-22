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
        private readonly IToDoList _ToDoList;

        public DataController(IWeather weather, IToDoList toDoList)
        {
            _Weather = weather;
            _ToDoList = toDoList;
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

        [HttpGet("GetToDoList")]
        public IActionResult GetToDoList()
        {
            try
            {
                return Ok(_ToDoList.GetToDoList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}