using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    public class WeatherModel
    {
        public string Status { get; set; }
        public Fact fact { get; set; }
        public List<Forecasts> forecasts { get; set; }
    }

    public class Fact
    {
        public string temp { get; set; }
        public string feels_like { get; set; }
        public string condition { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
    }

    public class Forecasts
    {
        public string date { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public Parts parts { get; set; }
    }

    public class Parts
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
