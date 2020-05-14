using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StatusDisplayClient.Services
{
    static class ToDo
    {
        static public List<ToDoListItem> GetToDoList(List<ToDoListItem> currentModel)
        {
            WebRequest request = WebRequest.Create("https://localhost:5001/api/Data/GetToDoList");
            WebResponse response = request.GetResponse();
            List<ToDoListItem> result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<ToDoListItem>>(json);
            }
            response.Close();

            foreach(var item in result)
            {
                ToDoListItem i = currentModel.Find(x => x.id == item.id);
                if (i != null)
                {
                    item.IsChecked = i.IsChecked;
                }
            }

            return result;
        }
    }
}
