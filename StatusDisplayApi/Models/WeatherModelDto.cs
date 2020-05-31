using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class WeatherModelDto
    {
        public FactDto fact { get; set; }
        public List<ForecastDto> forecasts { get; set; }
    }

    public class FactDto
    {
        public string temp { get; set; }
        public string feels_like { get; set; }
        public string condition { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
    }

    public class ForecastDto
    {
        public string date { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public PartsDto parts { get; set; }
    }

    public class PartsDto
    {
        public Night night { get; set; }
        public Morning morning { get; set; }
        public Day day { get; set; }
        public Evening evening { get; set; }
    }

    public class Night
    {
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string feels_like { get; set; }
        public string condition { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
        public string prec_prob { get; set; }
    }    
    
    public class Morning
    {
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string feels_like { get; set; }
        public string condition { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
        public string prec_prob { get; set; }
    }    
    
    public class Day
    {
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string feels_like { get; set; }
        public string condition { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
        public string prec_prob { get; set; }
        public string uv_index { get; set; }
    }    
    
    public class Evening
    {
        public string temp_min { get; set; }
        public string temp_max { get; set; }
        public string feels_like { get; set; }
        public string condition { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
        public string prec_prob { get; set; }
    }
}
