using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StatusDisplayClient.Services
{
    static class EngWord
    {
        public static async Task<EngTranslatedWordModel> GetEngWord()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };

            var response = await client.GetAsync("Data/GetEngWord");

           return JsonConvert.DeserializeObject<EngTranslatedWordModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
