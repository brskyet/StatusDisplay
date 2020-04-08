﻿using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.Models
{
    class ButtonStatModel
    {
        public bool IsTimerTicking { get; set; }
        public bool IsTimerStarted { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

    }
}
