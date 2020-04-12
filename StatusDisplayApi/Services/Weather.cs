using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class Weather : IWeather
    {
        private readonly ConfigJson config_json;
        private const string path = @"config.json";

        public Weather()
        {
            string config = File.ReadAllText(path);
            config_json = JsonConvert.DeserializeObject<ConfigJson>(config);
        }

        public WeatherModel GetForecast()
        {
            WebRequest request = WebRequest.Create($"https://api.weather.yandex.ru/v1/informers?" +
                $"lat={config_json.yandex_weather_lat}&lon={config_json.yandex_weather_lon}&lang=en_US");
            request.Headers.Add($"X-Yandex-API-Key: {config_json.yandex_weather_key}");
            WebResponse response = request.GetResponse();
            WeatherModel result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<WeatherModel>(json);
            }
            response.Close();

            return result;
        }
    }
}
