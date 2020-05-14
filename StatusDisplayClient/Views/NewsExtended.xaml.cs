using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StatusDisplayClient.Models;
using StatusDisplayClient.ViewModels;

namespace StatusDisplayClient.Views
{
    public class NewsExtended : Window
    {
        public NewsExtended()
        {

        }
        public NewsExtended(NewsModel model)
        {
            DataContext = new NewsExtendedViewModel(model, this);
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
