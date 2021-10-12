using Ninject;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;

namespace ProjectManager.UI.ViewModels
{
    class LanguageControlViewModel: BaseViewModel
    {
        private CultureInfo _selectedLang = LanguageManager.Language;
        private IMessenger _messenger;

        public LanguageControlViewModel()
        {
            _messenger = App.Container.Get<IMessenger>();
        }

        public IEnumerable<CultureInfo> Languages => LanguageManager.Languages;

        public CultureInfo SelectedLang
        {
            get => _selectedLang;
            set
            {
                if (value.Name == _selectedLang.Name) return;

                _selectedLang = value;
                LanguageManager.Language = value;

                if (_messenger.SendConfirmMessage(Properties.Resources.RestartAppConfirm))
                {
                    Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                    Application.Current.Shutdown();
                }

            }
        }
    }
}
