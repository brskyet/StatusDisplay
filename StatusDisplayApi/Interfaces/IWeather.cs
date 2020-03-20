using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Interfaces
{
    public interface IWeather
    {
        WeatherModel GetForecast();
    }
}
