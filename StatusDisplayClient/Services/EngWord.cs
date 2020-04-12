using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StatusDisplayClient.Services
{
    static class EngWord
    {
        public static EngTranslatedWordModel GetEngWord()
        {
            WebRequest request = WebRequest.Create("https://localhost:5001/api/Data/GetEngWord");
            WebResponse response = request.GetResponse();
            EngTranslatedWordModel result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<EngTranslatedWordModel>(json);
            }
            response.Close();
            return result;
        }
    }
}
