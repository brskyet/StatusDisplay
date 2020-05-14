using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class NewsModel
    {
        public List<SingleNews> Games { get; set; }
        public List<SingleNews> Index { get; set; }
    }

    public class SingleNews
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}
