using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static async Task<WeatherModel> GetForecast()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };

            var response = await client.GetAsync("Data/GetForecast");

            var model = JsonConvert.DeserializeObject<WeatherModel>(await response.Content.ReadAsStringAsync());

            // Replacement default values to readable values
            model.Facts.Condition = Condition[model.Facts.Condition];
            foreach (var f in model.Forecasts)
            {
                var date = DateTime.Parse(f.Date);
                f.Date = date.ToLongDateString();
                if (date.Day == DateTime.Now.Day)
                    f.Date += ",\nсегодня";
                else if (date.Day == DateTime.Now.AddDays(1).Day)
                    f.Date += ",\nзавтра";
                else
                    f.Date += (",\n" + date.ToString("dddd", new CultureInfo("ru-RU")));
                foreach(var p in f.Parts)
                {
                    p.Condition = Condition[p.Condition];
                }
            }
            model.Status = "Прогноз погоды на сегодня";

            return model;
        }
    }
}
