using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class ConfigJson
    {
        public string yandex_weather_key { get; set; }
        public string yandex_weather_lat { get; set; }
        public string yandex_weather_lon { get; set; }
        public string trello_dev_key { get; set; }
        public string trello_user_key { get; set; }
        public string trello_list_id { get; set; }
    }
}
