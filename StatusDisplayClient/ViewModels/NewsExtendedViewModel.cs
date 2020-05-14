using StatusDisplayClient.Models;
using StatusDisplayClient.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.ViewModels
{
    class NewsExtendedViewModel
    {
        public NewsModel NewsModel { get; }
        NewsExtended _window;
        public NewsExtendedViewModel(NewsModel model, NewsExtended window)
        {
            NewsModel = model;
            _window = window;
        }

        public void OnCloseButton()
        {
            _window.Close();
        }
    }
}
