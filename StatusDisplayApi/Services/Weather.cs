using AutoMapper;
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
        private readonly IMapper _mapper;

        public Weather(IMapper mapper)
        {
            string config = File.ReadAllText(path);
            config_json = JsonConvert.DeserializeObject<ConfigJson>(config);

            _mapper = mapper;
        }

        public WeatherModel GetForecast()
        {
            WebRequest request = WebRequest.Create($"https://api.weather.yandex.ru/v1/forecast?" +
                $"lat={config_json.yandex_weather_lat}&lon={config_json.yandex_weather_lon}&lang=en_US");
            request.Headers.Add($"X-Yandex-API-Key: {config_json.yandex_weather_key}");
            WebResponse response = request.GetResponse();
            WeatherModelDto resultDto;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                resultDto = JsonConvert.DeserializeObject<WeatherModelDto>(json);
            }
            response.Close();

            WeatherModel result = new WeatherModel()
            {
                Forecasts = new List<Forecast>()
            };
            // facts doesn't changes
            result.Facts = _mapper.Map<Facts>(resultDto.Fact);
            foreach(var f in resultDto.Forecasts)
            {
                result.Forecasts.Add(new Forecast()
                {
                    Date = f.Date,
                    Sunrise = f.Sunrise,
                    Sunset = f.Sunset,
                    Parts = new List<Part>()
                    {
                        // night is time between 00 and 06 AM
                        _mapper.Map<Part>(f.Parts.Night),
                        _mapper.Map<Part>(f.Parts.Morning),
                        _mapper.Map<Part>(f.Parts.Day),
                        _mapper.Map<Part>(f.Parts.Evening)
                    }
                });
            }
            // remove night from 1st day, because it's actually previous day
            result.Forecasts[0].Parts.RemoveAt(0);
            // now we need to take night from next day and put it in current day
            for(int i = result.Forecasts.Count - 1; i > 0 ; i--)
            {
                result.Forecasts[i - 1].Parts.Add(result.Forecasts[i].Parts[0]);
                // don't forget to remove this part of day
                result.Forecasts[i].Parts.RemoveAt(0);
                // at noon uv index is the most high
                result.Forecasts[i].UvIndex = resultDto.Forecasts[i].Parts.Day.Uv_index;
            }

            for (int i = 0; i <= result.Forecasts.Count - 1; i++)
            {
                // at noon uv index is the most high
                result.Forecasts[i].UvIndex = resultDto.Forecasts[i].Parts.Day.Uv_index;
            }

            result.Facts.UvIndex = result.Forecasts[0].UvIndex;

            return result;
        }
    }
}
