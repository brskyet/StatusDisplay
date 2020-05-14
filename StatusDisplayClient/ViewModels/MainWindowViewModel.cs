using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.ReactiveUI;
using StatusDisplayClient.Models;
using StatusDisplayClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using StatusDisplayClient.Views;

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

        private WidgetColor widgetColor;
        public WidgetColor WidgetColor
        {
            get { return widgetColor; }
            set
            {
                if (value != widgetColor)
                {
                    widgetColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private EngTranslatedWordModel engTranslatedWordModel;
        public EngTranslatedWordModel EngTranslatedWordModel
        {
            get { return engTranslatedWordModel; }
            set
            {
                if (value != engTranslatedWordModel)
                {
                    engTranslatedWordModel = value;
                    OnPropertyChanged();
                }
            }
        }

        private NewsModel newsModel;
        public NewsModel NewsModel
        {
            get { return newsModel; }
            set
            {
                if (value != newsModel)
                {
                    newsModel = value;
                    OnPropertyChanged();
                }
            }
        }

        // for timer widget
        private TimeSpan timer;
        private int timerHours, timerMinutes, timerSeconds;

        //for flash animation
        private int displayFlash;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DispatcherTimer timerForecast, timerToDo, timerTime, timerTimer, timerFlash, timerEngWord, timerNews;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            WeatherModel = new WeatherModel
            {
                Status = "Weather forecast is loading...",
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
                Status = "To-do list is loading...",
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
                ButtonColor = SolidColorBrush.Parse("#1A361F"),
                TextColor = SolidColorBrush.Parse("#56D45B")
            };

            WidgetColor = new WidgetColor
            {
                TimeColor = SolidColorBrush.Parse("#505356"),
                WeatherColor = SolidColorBrush.Parse("#3F4041"),
                TimerColor = SolidColorBrush.Parse("#282828"),
                TodoListColor = SolidColorBrush.Parse("#252328"),
                NewsColor = SolidColorBrush.Parse("#4E4C48"),
                EngWordColor = SolidColorBrush.Parse("#393E41")
            };

            EngTranslatedWordModel = new EngTranslatedWordModel
            {
                Status = "Word of the day is loading..."
            };

            NewsModel = new NewsModel
            {
                LatestTitle = "News are loading..."
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
                Interval = TimeSpan.FromSeconds(1)
            };
            timerTimer.Tick += OnTimedEventTimer;

            timerFlash = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(250)
            };
            timerFlash.Tick += OnTimedEventFlash;

            timerEngWord = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            timerEngWord.Tick += OnTimedEventEngWord;
            timerEngWord.Start();

            timerNews = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            timerNews.Tick += OnTimedEventNews;
            timerNews.Start();

            OnTimedEventForecast();
            OnTimedEventToDoList();
            OnTimedEventTime();
            OnTimedEventEngWord();
            OnTimedEventNews();
        }

        private async void OnTimedEventNews(object sender = null, EventArgs e = null)
        {
            try
            {
                var model = await Task.Run(News.GetNews);
                if(model.Index[0].Time.CompareTo(model.Games[0].Time) > 0)
                {
                    model.LatestTitle = model.Index[0].Time.ToString() + ": " + model.Index[0].Title;
                    model.LatestDescription = model.Index[0].Description;
                }
                else
                {
                    model.LatestTitle = model.Games[0].Time.ToString() + ": " + model.Games[0].Title;
                    model.LatestDescription = model.Games[0].Description;
                }
                NewsModel = model;
            }
            catch
            {
                var model = NewsModel;
                model.LatestTitle = "News: an error was occurred";
                NewsModel = model;
            }
        }

        private async void OnTimedEventEngWord(object sender = null, EventArgs e = null)
        {
            if (sender == null || DateTime.Now.ToLongTimeString() == "5:00:00") // call from ctor or if now is 5 AM
            {
                //section for To-do list
                var todomodel = ToDoListModel;
                foreach (var item in todomodel.toDoListItems)
                {
                    item.IsChecked = false;
                }
                ToDoListModel = todomodel;

                //section for word of day
                try
                {
                    var model = await Task.Run(EngWord.GetEngWord);
                    model.Status = "Word of the day: ";
                    EngTranslatedWordModel = model;
                }
                catch
                {
                    var model = EngTranslatedWordModel;
                    model.Status = "Word of the day: an error was occurred";
                    EngTranslatedWordModel = model;
                }
            }
        }

        private void OnTimedEventTimer(object sender = null, EventArgs e = null)
        {
            if (timer.TotalSeconds > 0)
            {
                timer -= new TimeSpan(0, 0, 1);
                TimerModel = new TimerModel { Hours = timer.Hours, Minutes = timer.Minutes, Seconds = timer.Seconds };
            }
            else
            {
                OnCancelButton();
                timerFlash.Start();
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
                var model = new WeatherModel { Status = "Weather forecast: an error was occurred", 
                    fact = WeatherModel.fact, forecasts = WeatherModel.forecasts };
                WeatherModel = model;
            }
        }

        private async void OnTimedEventToDoList(object sender = null, EventArgs e = null)
        {
            try
            {
                var toDoListItems = await Task.Run(() => ToDo.GetToDoList(ToDoListModel.toDoListItems));
                var model = new ToDoListModel { Status = "Список задач", toDoListItems = toDoListItems };
                ToDoListModel = model;
            }
            catch
            {
                var model = new ToDoListModel { Status = "To-do list: an error was occurred", toDoListItems = ToDoListModel.toDoListItems };
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
                var model = new DateTimeModel { Date = "An error was occurred", Time = "" };
                DateTimeModel = model;
            }
        }

        // trigger then mouse scrolling on hours input field
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
            if (ButtonStatModel.IsTimerStarted == false)
            {
                ButtonStatModel = new ButtonStatModel { IsTimerStarted = true, IsTimerTicking = true, Text = "Пауза", ButtonColor = SolidColorBrush.Parse("#3D2809"), TextColor = SolidColorBrush.Parse("#D67E00") };
                timerHours = TimerModel.Hours;
                timerMinutes = TimerModel.Minutes;
                timerSeconds = TimerModel.Seconds;
                timer = new TimeSpan(TimerModel.Hours, TimerModel.Minutes, TimerModel.Seconds);
                timerTimer.Start();
            }
            else
            {
                if (ButtonStatModel.IsTimerTicking == true)
                {
                    timerTimer.Stop();
                    ButtonStatModel = new ButtonStatModel { IsTimerStarted = true, IsTimerTicking = false, Text = "Дальше", ButtonColor = SolidColorBrush.Parse("#1A361F"), TextColor = SolidColorBrush.Parse("#56D45B") };
                }
                else
                {
                    timerTimer.Start();
                    ButtonStatModel = new ButtonStatModel { IsTimerStarted = true, IsTimerTicking = true, Text = "Пауза", ButtonColor = SolidColorBrush.Parse("#3D2809"), TextColor = SolidColorBrush.Parse("#D67E00") };
                }
            }
        }

        public void OnCancelButton()
        {
            timerTimer.Stop();
            ButtonStatModel = new ButtonStatModel { IsTimerStarted = false, IsTimerTicking = false, Text = "Старт", ButtonColor = SolidColorBrush.Parse("#1A361F"), TextColor = SolidColorBrush.Parse("#56D45B") };
            TimerModel = new TimerModel { Hours = timerHours, Minutes = timerMinutes, Seconds = timerSeconds };
        }

        public void OnEngWordClicked()
        {
            EngWordExtended window = new EngWordExtended(EngTranslatedWordModel);
            window.Show();
        }

        public void OnNewsClicked()
        {
            NewsExtended window = new NewsExtended(NewsModel);
            window.Show();
        }

        public void OnWeatherClicked()
        {
            WeatherExtended window = new WeatherExtended(WeatherModel);
            window.Show();
        }

        private void OnTimedEventFlash(object sender, EventArgs e)
        {
            // TODO: make avalonia animation instead this method
            if (displayFlash % 2 == 0)
                WidgetColor = new WidgetColor
                {
                    TimeColor = SolidColorBrush.Parse("#320100"),
                    WeatherColor = SolidColorBrush.Parse("#320100"),
                    TimerColor = SolidColorBrush.Parse("#320100"),
                    TodoListColor = SolidColorBrush.Parse("#320100"),
                    NewsColor = SolidColorBrush.Parse("#320100"),
                    EngWordColor = SolidColorBrush.Parse("#320100")
                };
            else
                WidgetColor = new WidgetColor
                {
                    TimeColor = SolidColorBrush.Parse("#505356"),
                    WeatherColor = SolidColorBrush.Parse("#3F4041"),
                    TimerColor = SolidColorBrush.Parse("#282828"),
                    TodoListColor = SolidColorBrush.Parse("#252328"),
                    NewsColor = SolidColorBrush.Parse("#4E4C48"),
                    EngWordColor = SolidColorBrush.Parse("#393E41")
                };
            displayFlash++;
            if(displayFlash >= 12)
            {
                displayFlash = 0;
                timerFlash.Stop();
            }
        }
    }
}
