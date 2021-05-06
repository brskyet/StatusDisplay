using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StatusDisplayClient.Services
{
    static class News
    {
        public static async Task<NewsModel> GetNews()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };

            var response = await client.GetAsync("Data/GetNews");

            return JsonConvert.DeserializeObject<NewsModel>(await response.Content.ReadAsStringAsync());
        }
    }
}
