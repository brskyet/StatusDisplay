using Newtonsoft.Json;
using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StatusDisplayClient.Services
{
    static class ToDo
    {
        public static async Task<List<ToDoListItem>> GetToDoList(List<ToDoListItem> currentModel)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/")
            };

            var response = await client.GetAsync("Data/GetToDoList");

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ToDoListItem>>(json);

            // Cycle for set checkboxes
            foreach (var item in result)
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
