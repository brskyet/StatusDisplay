using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class WeatherModelDto
    {
        public FactDto Fact { get; set; }
        public List<ForecastDto> Forecasts { get; set; }
    }

    public class FactDto
    {
        public string Temp { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
    }

    public class ForecastDto
    {
        public string Date { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public PartsDto Parts { get; set; }
    }

    public class PartsDto
    {
        public Night Night { get; set; }
        public Morning Morning { get; set; }
        public Day Day { get; set; }
        public Evening Evening { get; set; }
    }

    public class Night
    {
        public string Temp_min { get; set; }
        public string Temp_max { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
        public string Prec_prob { get; set; }
    }    
    
    public class Morning
    {
        public string Temp_min { get; set; }
        public string Temp_max { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
        public string Prec_prob { get; set; }
    }    
    
    public class Day
    {
        public string Temp_min { get; set; }
        public string Temp_max { get; set; }
        public string Feels_like { get; set; }
        public string Condition { get; set; }
        public string Wind_speed { get; set; }
        public string Pressure_mm { get; set; }
        public string Humidity { get; set; }
        public string Prec_prob { get; set; }
        public string Uv_index { get; set; }
    }    
    
    public class Evening
    {
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
