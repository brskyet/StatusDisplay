using System;
using AutoMapper;
using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class Weather : IWeather
    {
        private readonly ConfigJson config_json;
        private const string path = @"config.json";
        private readonly IMapper _mapper;

        public Weather(IMapper mapper)
        {
            var config = File.ReadAllText(path);
            config_json = JsonConvert.DeserializeObject<ConfigJson>(config);

            _mapper = mapper;
        }

        public async Task<WeatherModel> GetForecast()
        {
            var request = WebRequest.Create($"https://api.weather.yandex.ru/v1/informers?" +
                                            $"lat={config_json.yandex_weather_lat}&lon={config_json.yandex_weather_lon}&lang=en_US");
            request.Headers.Add($"X-Yandex-API-Key: {config_json.yandex_weather_key}");
            var response = request.GetResponse();
            WeatherModel result;
            using (var dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);
                var json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<WeatherModel>(json);
            }
            response.Close();

            return result;
        }
    }
}
