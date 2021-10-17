using System;
using System.Windows;
using Ninject;
using ProjectManager.UI.Common;
using ProjectManager.UI.Models;
using ProjectManager.UI.Services;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;

namespace ProjectManager.UI.ViewModels
{
    public class AuthenticationViewModel
    {
        private IAuthenticationService _authService;
        private IMessenger _messenger;

        private RelayCommand _authCommand;

        public string AuthName { get; set; }
        public string AuthPassword { get; set; }

        public AuthenticationViewModel()
        {
            _authService = App.Container.Get<IAuthenticationService>();
            _messenger = App.Container.Get<IMessenger>();
        }

        public RelayCommand AuthCommand
        {
            get
            {
                return _authCommand ?? (_authCommand = new RelayCommand( _ =>  Auth()));
            }
        }

        private async void Auth()
        {
            try
            {
                var dataIsEmpty =
                    (String.IsNullOrWhiteSpace(AuthName) || 
                     String.IsNullOrWhiteSpace(AuthPassword));

                if (dataIsEmpty)
                {
                    _messenger.SendMessage(Properties.Resources.EmptyData);
                    return;
                }

                await _authService.AuthorizeAsync(AuthName, AuthPassword);

                var win = (MainWindow)Application.Current.MainWindow;
                win.Frame.Navigate(new MainPage());
            }
            catch (UnauthorizedAccessException)
            {
                _messenger.SendMessage(Properties.Resources.InvalidLoginOrPassword);
            }
            catch (Exception)
            {
                _messenger.SendMessage(Properties.Resources.ErrorOfenter);
            }
        }
    }
}