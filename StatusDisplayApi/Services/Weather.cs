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
        public WeatherModel GetForecast()
        {
            //https://api.weather.yandex.ru/v1/informers?lat=55.6409636&lon=37.6073091
            WebRequest request = WebRequest.Create("https://api.weather.yandex.ru/v1/informers?lat=55.6409636&lon=37.6073091&lang=en_US");
            request.Headers.Add("X-Yandex-API-Key: 0f59475d-07bc-4907-a313-d57555cc02f0");
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
