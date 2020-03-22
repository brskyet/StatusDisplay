using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    public class ToDoListModel
    {
        public List<ToDoListItem> toDoListItems { get; set; }
        public string Status { get; set; }
    }

    public class ToDoListItem
    {
        public string name { get; set; }
        public Badges badges { get; set; }
    }

    public class Badges
    {
        public string due { get; set; }
    }
}
