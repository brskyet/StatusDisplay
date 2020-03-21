using Avalonia.Threading;
using StatusDisplayClient.Models;
using StatusDisplayClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;

namespace StatusDisplayClient.ViewModels
{
    class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherModel weatherModel;

        public WeatherModel WeatherModel
        {
            get { return weatherModel; }
            set
            {
                if (value != weatherModel)
                {
                    weatherModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Forecast _forecast;
        private DispatcherTimer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherViewModel(Forecast forecast)
        {
            _forecast = forecast;
            WeatherModel = new WeatherModel
            {
                fact = new Fact 
                { 
                    temp = "Loading...", feels_like = "Loading...", humidity = "Loading...", pressure_mm = "Loading...", wind_speed = "Loading..." 
                }
            };
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromHours(1)
            };
            timer.Tick += OnTimedEvent;
            timer.Start();
            OnTimedEvent();
        }

        private void OnTimedEvent(object sender = null, EventArgs e = null)
        {
            try
            {
                var model = _forecast.GetForecast();
                model.fact.temp += "°";
                model.fact.feels_like += "°";
                model.fact.wind_speed += " м/с";
                model.fact.pressure_mm += " мм рт.ст.";
                model.fact.humidity += "%";
                weatherModel = model;
            }
            catch
            {
                weatherModel = new WeatherModel { fact = new Fact { temp = "Error!" } };
            }
        }
    }
}
