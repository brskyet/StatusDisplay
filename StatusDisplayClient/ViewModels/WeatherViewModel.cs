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
    class MainWindowViewModel : INotifyPropertyChanged
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

        private ToDoListModel toDoListModel;
        public ToDoListModel ToDoListModel
        {
            get { return toDoListModel; }
            set
            {
                if (value != toDoListModel)
                {
                    toDoListModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly DispatcherTimer timerForecast, timerToDo;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            WeatherModel = new WeatherModel
            {
                Status = "Weather forecast",
                fact = new Fact
                {
                    temp = "Loading...",
                    feels_like = "Loading...",
                    humidity = "Loading...",
                    pressure_mm = "Loading...",
                    wind_speed = "Loading..."
                }
            };

            ToDoListModel = new ToDoListModel
            {
                Status = "To do list",
                toDoListItems = new List<ToDoListItem>()
            };

            timerForecast = new DispatcherTimer
            {
                Interval = TimeSpan.FromHours(1)
            };
            timerForecast.Tick += OnTimedEventForecast;
            timerForecast.Start();

            timerToDo = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timerToDo.Tick += OnTimedEventToDoList;
            timerToDo.Start();
            OnTimedEventForecast();
            OnTimedEventToDoList();
        }

        private void OnTimedEventForecast(object sender = null, EventArgs e = null)
        {
            try
            {
                var model = Forecast.GetForecast();
                model.fact.temp += "°";
                model.fact.feels_like += "°";
                model.fact.wind_speed += " м/с";
                model.fact.pressure_mm += " мм рт.ст.";
                model.fact.humidity += "%";
                WeatherModel = model;
            }
            catch
            {
                var model = new WeatherModel { Status = "Weather forecast: an error occurred", fact = WeatherModel.fact };
                WeatherModel = model;
            }
        }

        private void OnTimedEventToDoList(object sender = null, EventArgs e = null)
        {
            try
            {
                var toDoListItems = ToDo.GetToDoList();
                var model = new ToDoListModel { Status = "To do list", toDoListItems = toDoListItems };
                ToDoListModel = model;
            }
            catch
            {
                var model = new ToDoListModel { Status = "To do list: an error occurred", toDoListItems = ToDoListModel.toDoListItems };
                ToDoListModel = model;
            }
        }
    }
}
