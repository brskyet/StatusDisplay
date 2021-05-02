using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StatusDisplayClient.Models;
using StatusDisplayClient.ViewModels;

namespace StatusDisplayClient.Views
{
    public class WeatherExtended : Window
    {
        public WeatherExtended()
        {

        }

        public WeatherExtended(WeatherModel model)
        {
            DataContext = new WeatherExtendedViewModel(model, this);
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
