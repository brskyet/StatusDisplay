using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class ToDoListModel
    {
        public string name { get; set; }
        public Badges badges { get; set; }
    }

    public class Badges 
    {
        public string due { get; set; }
    }
}
