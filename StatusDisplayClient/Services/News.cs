﻿using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StatusDisplayClient.Services
{
    static class News
    {
        public static NewsModel GetNews()
        {
            WebRequest request = WebRequest.Create("https://localhost:5001/api/Data/GetNews");
            WebResponse response = request.GetResponse();
            NewsModel result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<NewsModel>(json);
            }
            response.Close();
            return result;
        }
    }
}
