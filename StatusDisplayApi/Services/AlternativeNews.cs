using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StatusDisplayApi.Services
{
    public class AlternativeNews : INews
    {
        public async Task<NewsModel> GetNews()
        {
            var indexURL = "http://static.feed.rbc.ru/";
            return new NewsModel {Index = await GetCategoryAsync(indexURL)};
        }

        private async Task<List<SingleNews>> GetCategoryAsync(string url)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            var result = await client.GetAsync("rbc/logical/footer/news.rss");
            var xml = await result.Content.ReadAsStringAsync();

            var doc = XDocument.Parse(xml);

            var channel = doc.Descendants("channel").ToList();

            var news = channel.Descendants("item").Select(r => new SingleNews()
            {
                Title = r.Element("title")?.Value,
                Description = r.Element("description")?.Value,
                Time = DateTime.Parse(r.Element("pubDate")?.Value)
            }).ToList();

            return news.OrderByDescending(n => n.Time).ToList();
        }
    }
}