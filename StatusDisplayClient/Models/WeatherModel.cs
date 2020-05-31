using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    public class WeatherModel
    {
        public string Status { get; set; }
        public Facts Facts { get; set; }
        public List<Forecasts> Forecasts { get; set; }
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

    public class Forecasts
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
        public string Temp_avg { get; set; }
        public string Temp_str { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
        public string Prec_prob { get; set; }
    }
}
