using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace StatusDisplayApi.Services
{
    public class News : INews
    {
        public NewsModel GetNews()
        {
            string indexURL = "https://news.yandex.ru/index.rss";
            string gamesURL = "https://news.yandex.ru/games.rss";
            return new NewsModel() { Index = GetCategory(indexURL), Games = GetCategory(gamesURL) };
        }

        private List<SingleNews> GetCategory(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            string result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
            }

            XDocument doc = XDocument.Parse(result);
            var channel = doc.Descendants("channel").ToList();

            var news = channel.Descendants("item").Select(r => new SingleNews()
            {
                Title = r.Element("title").Value,
                Description = r.Element("description").Value,
                Time = DateTime.Parse(r.Element("pubDate").Value.Replace(" +0000", ""))

            }).ToList();

            return news.OrderByDescending(n => n.Time).ToList();
        }
    }
}
