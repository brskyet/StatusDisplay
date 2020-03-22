using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    public class WeatherModel
    {
        public string Status { get; set; }
        public Fact fact { get; set; }
    }

    public class Fact
    {
        public string temp { get; set; }
        public string feels_like { get; set; }
        public string wind_speed { get; set; }
        public string pressure_mm { get; set; }
        public string humidity { get; set; }
    }
}
