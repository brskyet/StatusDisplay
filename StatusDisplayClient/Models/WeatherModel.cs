using System.Collections.Generic;

namespace StatusDisplayClient.Models
{
    public class WeatherModel
    {
        public string Status { get; set; }
        public Fact Fact { get; set; }
        public Forecast Forecast { get; set; }
    }

    public class Fact
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
        public List<Part> Parts { get; set; }
    }

    public class Part
    {
        public string Part_name { get; set; }
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
