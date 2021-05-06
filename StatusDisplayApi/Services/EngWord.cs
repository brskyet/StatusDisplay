using System;
using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System.IO;
using System.Net;
using System.Net.Http;
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

        public async Task<EngWordModel> GetOriginalWord()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.wordnik.com/v4/words.json/wordOfTheDay" +
                                      $"?api_key={config_json.wordnik_api_key}")
            };

            var response = await client.GetAsync("");

            return JsonConvert.DeserializeObject<EngWordModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
