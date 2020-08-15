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
using System.Text.RegularExpressions;

namespace StatusDisplayApi.Services
{
    public class News : INews
    {
        public NewsModel GetNews()
        {
            const string indexUrl = "https://news.yandex.ru/index.rss";
            const string gamesUrl = "https://dtf.ru/rss/all";
            return new NewsModel() { Index = GetCategory(indexUrl), Games = GetCategory(gamesUrl) };
        }

        private List<SingleNews> GetCategory(string url)
        {
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            string result;
            using (var dataStream = response.GetResponseStream())
            {
                var reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
            }

            var doc = XDocument.Parse(result);
            var channel = doc.Descendants("channel").ToList();

            var news = channel.Descendants("item").Select(r => new SingleNews()
            {
                Title = r.Element("title").Value,
                Description = Regex.Replace(r.Element("description").Value, @"\n\s*<img.*", "").Replace(@"&quot;", "'"),
                Time = DateTime.Parse(r.Element("pubDate").Value, System.Globalization.CultureInfo.InvariantCulture)
            }).ToList();

            return news.OrderByDescending(n => n.Time).ToList();
        }
    }
}
