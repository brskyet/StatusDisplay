using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    class WidgetColor
    {
        public IBrush TimeColor { get; set; }
        public IBrush WeatherColor { get; set; }
        public IBrush TimerColor { get; set; }
        public IBrush TodoListColor { get; set; }
        public IBrush NewsColor { get; set; }
        public IBrush EngWordColor { get; set; }
    }
}
