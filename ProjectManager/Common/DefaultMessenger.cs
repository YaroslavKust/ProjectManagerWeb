using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.UI
{
    class DefaultMessenger: IMessenger
    {
        public async void SendMessage(string text)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            await window.ShowMessageAsync("", text);
        }

        public async Task<bool> SendConfirmMessageAsync(string text)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = Properties.Resources.Ok,
                NegativeButtonText = Properties.Resources.Cansel
            };

            var window = Application.Current.MainWindow as MetroWindow;
            var res = await window.ShowMessageAsync("", text,MessageDialogStyle.AffirmativeAndNegative, settings);
            return res == MessageDialogResult.Affirmative;
        }
    }
}
