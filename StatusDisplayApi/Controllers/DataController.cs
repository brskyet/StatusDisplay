using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<WeatherModel> GetForecast()
        {
            return await _Weather.GetForecast();
        }

        [HttpGet("GetToDoList")]
        public async Task<List<ToDoListModel>> GetToDoList()
        {
            return await _ToDoList.GetToDoList();
        }

        [HttpGet("GetEngWord")]
        public async Task<EngTranslatedWordModel> GetEngWord()
        {
            var originalWord = await _EngWord.GetOriginalWord();
            return await _Translate.GetTranslation(originalWord);
        }

        [HttpGet("GetNews")]
        public async Task<NewsModel> GetNews()
        {
            return await _News.GetNews();
        }
    }
}