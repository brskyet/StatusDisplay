using Avalonia.Controls;
using Avalonia.Threading;
using StatusDisplayClient.Models;
using StatusDisplayClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        private DateTimeModel dateTimeModel;
        public DateTimeModel DateTimeModel
        {
            get { return dateTimeModel; }
            set
            {
                if (value != dateTimeModel)
                {
                    dateTimeModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimerModel timerModel;
        public TimerModel TimerModel
        {
            get { return timerModel; }
            set
            {
                if (value != timerModel)
                {
                    timerModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private ButtonStatModel buttonStatModel;
        public ButtonStatModel ButtonStatModel
        {
            get { return buttonStatModel; }
            set
            {
                if (value != buttonStatModel)
                {
                    buttonStatModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimeSpan timer;
        private int timerHours, timerMinutes, timerSeconds;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private  DispatcherTimer timerForecast, timerToDo, timerTime, timerTimer;

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
                Status = "Список задач",
                toDoListItems = new List<ToDoListItem>()
            };

            TimerModel = new TimerModel
            {
                Hours = 0,
                Minutes = 0,
                Seconds = 0
            };

            ButtonStatModel = new ButtonStatModel
            {
                IsTimerStarted = false,
                IsTimerTicking = false,
                Text = "Старт",
                Color = new Avalonia.Media.Color(1, 61, 40 ,9)
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

            timerTime = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timerTime.Tick += OnTimedEventTime;
            timerTime.Start();

            timerTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1), IsEnabled = false
            };
            timerTimer.Tick += OnTimedEventTimer;

            OnTimedEventForecast();
            OnTimedEventToDoList();
            OnTimedEventTime();
        }

        private void OnTimedEventTimer(object sender = null, EventArgs e = null)
        {
            if(timer.TotalSeconds > 0)
            {
                timer -= new TimeSpan(0, 0, 1);
                TimerModel = new TimerModel { Hours = timer.Hours, Minutes = timer.Minutes, Seconds = timer.Seconds };
            }
            else
            {
                OnCancelButton();
            }
        }

        private async void OnTimedEventForecast(object sender = null, EventArgs e = null)
        {
            try
            {
                var model = await Task.Run(Forecast.GetForecast);
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

        private async void OnTimedEventToDoList(object sender = null, EventArgs e = null)
        {
            try
            {
                var toDoListItems = await Task.Run(ToDo.GetToDoList);
                var model = new ToDoListModel { Status = "Список задач", toDoListItems = toDoListItems };
                ToDoListModel = model;
            }
            catch
            {
                var model = new ToDoListModel { Status = "To-do list: an error occurred", toDoListItems = ToDoListModel.toDoListItems };
                ToDoListModel = model;
            }
        }

        private void OnTimedEventTime(object sender = null, EventArgs e = null)
        {
            try
            {
                DateTime datetime = DateTime.Now;
                var model = new DateTimeModel { Date = datetime.ToString("dddd", new CultureInfo("ru-RU")) + ", " + datetime.ToLongDateString(), Time = datetime.ToShortTimeString() };
                DateTimeModel = model;
            }
            catch
            {
                var model = new DateTimeModel { Date = "An error occurred", Time = "" };
                DateTimeModel = model;
            }
        }

        public void HoursScroll(object sender, object parameter)
        {
            var param = (Avalonia.Input.PointerWheelEventArgs)parameter;
            if (param.Delta.Y > 0 && TimerModel.Hours < 23)
            {
                TimerModel = new TimerModel { Hours = TimerModel.Hours += 1, Minutes = TimerModel.Minutes, Seconds = TimerModel.Seconds };
            }
            else if (param.Delta.Y < 0 && TimerModel.Hours > 0)
            {
                TimerModel = new TimerModel { Hours = TimerModel.Hours -= 1, Minutes = TimerModel.Minutes, Seconds = TimerModel.Seconds };
            }
        }

        public void MinutesScroll(object sender, object parameter)
        {
            var param = (Avalonia.Input.PointerWheelEventArgs)parameter;
            if (param.Delta.Y > 0 && TimerModel.Minutes < 59)
            {
                TimerModel = new TimerModel { Hours = TimerModel.Hours, Minutes = TimerModel.Minutes += 1, Seconds = TimerModel.Seconds };
            }
            else if (param.Delta.Y < 0 && TimerModel.Minutes > 0)
            {
                TimerModel = new TimerModel { Hours = TimerModel.Hours, Minutes = TimerModel.Minutes -= 1, Seconds = TimerModel.Seconds };
            }
        }

        public void SecondsScroll(object sender, object parameter)
        {
            var param = (Avalonia.Input.PointerWheelEventArgs)parameter;
            if (param.Delta.Y > 0 && TimerModel.Seconds < 59)
            {
                TimerModel = new TimerModel { Hours = TimerModel.Hours, Minutes = TimerModel.Minutes, Seconds = TimerModel.Seconds += 1 };
            }
            else if (param.Delta.Y < 0 && TimerModel.Seconds > 0)
            {
                TimerModel = new TimerModel { Hours = TimerModel.Hours, Minutes = TimerModel.Minutes, Seconds = TimerModel.Seconds -= 1 };
            }
        }

        public void OnStartButton()
        {
            if (TimerModel.Hours == 0 && TimerModel.Minutes == 0 && TimerModel.Seconds == 0)
                return;
            if(ButtonStatModel.IsTimerStarted == false)
            {
                ButtonStatModel = new ButtonStatModel { IsTimerStarted = true, IsTimerTicking = true, Text = "Пауза", Color = new Avalonia.Media.Color(1, 26, 54, 31) };
                timerHours = TimerModel.Hours;
                timerMinutes = TimerModel.Minutes;
                timerSeconds = TimerModel.Seconds;
                timer = new TimeSpan(TimerModel.Hours, TimerModel.Minutes, TimerModel.Seconds);
                timerTimer.Start();
            }
            else
            {
                if(ButtonStatModel.IsTimerTicking == true)
                {
                    timerTimer.Stop();
                    ButtonStatModel = new ButtonStatModel { IsTimerStarted = true, IsTimerTicking = false, Text = "Дальше", Color = new Avalonia.Media.Color(1, 61, 40, 9) };
                }
                else
                {
                    timerTimer.Start();
                    ButtonStatModel = new ButtonStatModel { IsTimerStarted = true, IsTimerTicking = true, Text = "Пауза", Color = new Avalonia.Media.Color(1, 26, 54, 31) };
                }
            }
        }

        public void OnCancelButton()
        {
            timerTimer.Stop();
            ButtonStatModel = new ButtonStatModel { IsTimerStarted = false, IsTimerTicking = false, Text = "Старт", Color = new Avalonia.Media.Color(1, 61, 40, 9) };
            TimerModel = new TimerModel { Hours = timerHours, Minutes = timerMinutes, Seconds = timerSeconds };
        }
    }
}
