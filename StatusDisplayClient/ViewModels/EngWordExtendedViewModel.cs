using StatusDisplayClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatusDisplayClient.ViewModels
{
    class EngWordExtendedViewModel
    {
        public EngTranslatedWordModel EngTranslatedWordModel { get; }
        EngWordExtended _window;
        public EngWordExtendedViewModel(EngTranslatedWordModel model, EngWordExtended window)
        {
            EngTranslatedWordModel = model;
            _window = window;
        }

        public void OnCloseButton()
        {
            _window.Close();
        }
    }
}
