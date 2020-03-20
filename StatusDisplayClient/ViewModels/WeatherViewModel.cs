using StatusDisplayClient.Models;
using StatusDisplayClient.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.ViewModels
{
    class WeatherViewModel
    {
        public WeatherModel weatherModel { get; }

        public WeatherViewModel(Forecast forecast)
        {
            try
            {
                weatherModel = forecast.GetForecast();
            }
            catch
            {
                weatherModel = new WeatherModel { fact = new Fact { temp = "Error!" } };
            }
        }
    }
}
