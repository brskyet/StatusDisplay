using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StatusDisplayClient.Services;
using StatusDisplayClient.ViewModels;

namespace StatusDisplayClient
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
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
