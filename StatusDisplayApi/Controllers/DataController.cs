using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;

namespace StatusDisplayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IWeather _Weather;
        private readonly IToDoList _ToDoList;
        private readonly IEngWord _EngWord;
        private readonly ITranslate _Translate;
        private readonly INews _News;

        public DataController(IWeather weather, IToDoList toDoList, IEngWord engword, ITranslate translate, INews news)
        {
            _Weather = weather;
            _ToDoList = toDoList;
            _EngWord = engword;
            _Translate = translate;
            _News = news;
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

        [HttpGet("GetEngWord")]
        public IActionResult GetEngWord()
        {
            try
            {
                var originalWord = _EngWord.GetOriginalWord();
                return Ok(_Translate.GetTranslation(originalWord));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetNews")]
        public IActionResult GetNews()
        {
            try
            {
                return Ok(_News.GetNews());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}