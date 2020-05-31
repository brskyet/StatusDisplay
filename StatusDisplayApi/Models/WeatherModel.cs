using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class WeatherModel
    {
        public Facts Facts { get; set; }
        public List<Forecast> Forecasts { get; set; }
    }

    public class Facts
    {
        public string Temp { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
        public string UvIndex { get; set; }
    }

    public class Forecast
    {
        public string Date { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string UvIndex { get; set; }
        public List<Part> Parts { get; set; }
    }

    public class Part
    {
        public string PartOfDay { get; set; }
        public string Temp_min { get; set; }
        public string Temp_max { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
        public string Prec_prob { get; set; }
    }
}
