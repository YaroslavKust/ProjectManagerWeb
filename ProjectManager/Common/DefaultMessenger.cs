using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.UI
{
    class DefaultMessenger: IMessenger
    {
        public void SendMessage(string text)
        {
            MessageBox.Show(text);
        }

        public bool SendConfirmMessage(string text)
        {
            var res = MessageBox.Show(text, "Confirmation", MessageBoxButton.YesNo);
            return res == MessageBoxResult.Yes;
        }
    }
}
