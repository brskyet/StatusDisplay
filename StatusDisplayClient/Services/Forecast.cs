using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StatusDisplayClient.Services
{
    class Forecast
    {
        public WeatherModel GetForecast()
        {
            WebRequest request = WebRequest.Create("https://localhost:5001/api/Data/GetForecast");
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
