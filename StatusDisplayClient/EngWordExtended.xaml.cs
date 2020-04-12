using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StatusDisplayClient.Models;
using StatusDisplayClient.ViewModels;

namespace StatusDisplayClient
{
    public class EngWordExtended : Window
    {
        public EngWordExtended()
        {

        }

        public EngWordExtended(EngTranslatedWordModel model)
        {
            DataContext = new EngWordExtendedViewModel(model, this);
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
