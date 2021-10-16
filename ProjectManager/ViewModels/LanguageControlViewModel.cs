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
                SetLang(value);
            }
        }

        private async void SetLang(CultureInfo info)
        {
            if (info.Name == _selectedLang.Name) return;

            _selectedLang = info;
            LanguageManager.Language = info;

            if (await _messenger.SendConfirmMessageAsync(Properties.Resources.RestartAppConfirm))
            {
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Application.Current.Shutdown();
            }
        }
    }
}
