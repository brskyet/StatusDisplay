using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class ToDoList : IToDoList
    {
        private readonly ConfigJson config_json;
        private const string path = @"config.json";

        public ToDoList()
        {
            string config = File.ReadAllText(path);
            config_json = JsonConvert.DeserializeObject<ConfigJson>(config);
        }

        public List<ToDoListModel> GetToDoList()
        {
            WebRequest request = WebRequest.Create(
                $"https://api.trello.com/1/lists/{config_json.trello_list_id}/cards?" +
                $"key={config_json.trello_dev_key}&token={config_json.trello_user_key}");
            WebResponse response = request.GetResponse();
            List<ToDoListModel> result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<ToDoListModel>>(json);
            }
            response.Close();

            return result;
        }
    }
}
