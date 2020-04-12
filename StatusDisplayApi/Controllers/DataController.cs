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

        public DataController(IWeather weather, IToDoList toDoList, IEngWord engword, ITranslate translate)
        {
            _Weather = weather;
            _ToDoList = toDoList;
            _EngWord = engword;
            _Translate = translate;
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
                originalWord = new EngWordModel { word = "antichthon", note = "The word 'antichthon' comes from Greek roots meaning 'against' and 'the earth'.",
                definitions = new List<Definition> { new Definition {
                    text = "In Pythagorean astronomy, an imaginary invisible planet continually opposing the earth and eclipsing the central fire, round which it was supposed to revolve, in common with the earth, moon, sun, certain planets, and the fixed stars.",
                    note = "",
                    partOfSpeech = "noun"
                }, new Definition { text = "plural The inhabitants of an opposite hemisphere.",
                note = "",
                partOfSpeech = "noun"} },
                examples = new List<Example> { new Example { 
                title = "Tentation de saint Antoine. English",
                text = "No longer will you see the antichthon of Plato, the focus of Philolaüs, the spheres of Aristotle, or the seven heavens of the Jews with the great waters above the vault of crystal!"}, 
                    new Example { title = "A System Of Logic, Ratiocinative And Inductive",
                    text = "The Pythagoreans, on the other hand, attributed perfection to the number ten; but agreed in thinking that the perfect number must be somehow realized in the heavens; and knowing only of nine heavenly bodies, to make up the enumeration, they asserted “that there was an _antichthon_, or counter-earth, on the other side of the sun, invisible to us.” "} }
                };
                return Ok(_Translate.GetTranslation(originalWord));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}