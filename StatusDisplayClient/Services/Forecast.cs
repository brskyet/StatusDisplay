using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace StatusDisplayClient.Services
{
    static class Forecast
    {
        static Dictionary<string, string> Condition = new Dictionary<string, string>
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
        static public WeatherModel GetForecast()
        {
            WebRequest request = WebRequest.Create("https://localhost:5001/api/Data/GetForecast");
            WebResponse response = request.GetResponse();
            WeatherModel model;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                model = JsonConvert.DeserializeObject<WeatherModel>(json);
            }
            response.Close();

            model.fact.condition = Condition[model.fact.condition];
            foreach (var forecast in model.forecasts)
            {
                var date = DateTime.Parse(forecast.date);
                forecast.date = date.ToLongDateString();
                if (date.Day == DateTime.Now.Day)
                    forecast.date += ",\nсегодня";
                else if (date.Day == DateTime.Now.AddDays(1).Day)
                    forecast.date += ",\nзавтра";
                else
                    forecast.date += (",\n" + date.ToString("dddd", new CultureInfo("ru-RU")));
                forecast.parts.night.condition = Condition[forecast.parts.night.condition];
                forecast.parts.morning.condition = Condition[forecast.parts.morning.condition];
                forecast.parts.day.condition = Condition[forecast.parts.day.condition];
                forecast.parts.evening.condition = Condition[forecast.parts.evening.condition];
            }
            model.Status = "Прогноз погоды на сегодня";

            return model;
        }
    }
}
