using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class EngWord : IEngWord
    {
        private readonly ConfigJson config_json;
        private const string path = @"config.json";

        public EngWord()
        {
            string config = File.ReadAllText(path);
            config_json = JsonConvert.DeserializeObject<ConfigJson>(config);
        }

        public EngWordModel GetOriginalWord()
        {
            WebRequest request = WebRequest.Create($"https://api.wordnik.com/v4/" +
                $"words.json/wordOfTheDay?api_key={config_json.wordnik_api_key}");
            WebResponse response = request.GetResponse();
            EngWordModel result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<EngWordModel>(json);
            }
            response.Close();

            return result;
        }
    }
}
