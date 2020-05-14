using StatusDisplayClient.Models;
using StatusDisplayClient.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.ViewModels
{
    class WeatherExtendedViewModel
    {
        public WeatherModel WeatherModel { get; }
        WeatherExtended _window;

        public WeatherExtendedViewModel(WeatherModel model, WeatherExtended window)
        {
            WeatherModel = model;
            _window = window;
        }

        public void OnCloseButton()
        {
            _window.Close();
        }
    }
}
