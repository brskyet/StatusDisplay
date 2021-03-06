﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    public class NewsModel
    {
        public List<SingleNews> Games { get; set; }
        public List<SingleNews> Index { get; set; }
        public string LatestTitle { get; set; }
        public string LatestDescription { get; set; }
    }

    public class SingleNews
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}
