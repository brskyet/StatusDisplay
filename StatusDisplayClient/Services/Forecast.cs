using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StatusDisplayClient.Services
{
    static class Forecast
    {
        // Dictionary for replacement
        static readonly Dictionary<string, string> Condition = new Dictionary<string, string>
        {
            {"clear", "Ясно"},
            {"partly-cloudy", "Малооблачно"},
            {"cloudy", "Облачно с прояснениями"},
            {"overcast", "Пасмурно"},
            {"partly-cloudy-and-rain", "Дождь, малооблачно"},
            {"overcast-and-rain", "Сильный дождь, пасмурно"},
            {"overcast-thunderstorms-with-rain", "Сильный дождь, гроза"},
            {"cloudy-and-light-rain", "Небольшой дождь, облачно"},
            {"overcast-and-light-rain", "Небольшой дождь, пасмурно"},
            {"partly-cloudy-and-light-rain", "Небольшой дождь, малооблачно"},
            {"cloudy-and-rain", "Дождь, облачно"},
            {"overcast-and-wet-snow", "Дождь со снегом, пасмурно"},
            {"partly-cloudy-and-snow", "Снег, малооблачно"},
            {"cloudy-and-snow", "Снег, облачно"},
            {"overcast-and-snow", "Снегопад, пасмурно"},
            {"cloudy-and-light-snow", "Небольшой снег, облачно"},
            {"overcast-and-light-snow", "Небольшой снег, пасмурно"},
            {"partly-cloudy-and-light-snow", "Небольшой снег, малооблачно"}
        };

        static readonly Dictionary<string, string> PartOfDay = new Dictionary<string, string>
        {
            {"night", "Ночь"},
            {"morning", "Утро"},
            {"day", "День"},
            {"evening", "Вечер"}
        };

        public static async Task<WeatherModel> GetForecast()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };

            var response = await client.GetAsync("Data/GetForecast");

            var json = await response.Content.ReadAsStringAsync();

            var model = JsonConvert.DeserializeObject<WeatherModel>(json);

            // Replacement default values to readable values
            model.Fact.Condition = Condition[model.Fact.Condition];
            foreach (var p in model.Forecast.Parts)
            {
                p.Condition = Condition[p.Condition];
                p.Part_name = PartOfDay[p.Part_name];
            }

            model.Status = "Сейчас на улице:";

            return model;
        }
    }
}