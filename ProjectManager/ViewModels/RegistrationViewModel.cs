using System;
using System.Windows;
using System.Windows.Input;
using Ninject;
using ProjectManager.UI.Common;
using ProjectManager.UI.Services;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;

namespace ProjectManager.UI.ViewModels
{
    public class RegistrationViewModel
    {
        private IAuthenticationService _authService;
        private IMessenger _messenger;

        private RelayCommand  _registerCommand;

        public string RegName { get; set; }
        public string RegPassword { get; set; }
        public string ConfirmedPassword { get; set; }

        public RegistrationViewModel()
        {
            _authService = App.Container.Get<IAuthenticationService>();
            _messenger = App.Container.Get<IMessenger>();
        }

        public RelayCommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(async _ =>
                {
                    var dataIsEmpty =
                        (String.IsNullOrWhiteSpace(RegName) ||
                         String.IsNullOrWhiteSpace(RegPassword) ||
                         String.IsNullOrWhiteSpace(ConfirmedPassword));

                    if (dataIsEmpty)
                    {
                        _messenger.SendMessage(Properties.Resources.EmptyData);
                        return;
                    }

                    if (ConfirmedPassword != RegPassword)
                    {
                        _messenger.SendMessage(Properties.Resources.DifferentPasswords);
                        return;
                    }

                    if (RegName.Length < 4)
                    {
                        _messenger.SendMessage(Properties.Resources.TooShortName);
                        return;
                    }

                    if(RegPassword.Length < 4)
                    {
                        _messenger.SendMessage(Properties.Resources.TooShortPassword);
                        return;
                    }

                    Mouse.OverrideCursor = Cursors.Wait;

                    try
                    {
                        await _authService.RegisterAsync(RegName, RegPassword);
                        Mouse.OverrideCursor = Cursors.Arrow;
                    }
                    catch(Exception)
                    {
                        Mouse.OverrideCursor = Cursors.Arrow;
                        _messenger.SendMessage(Properties.Resources.NameAlreadyUsed);
                        return;
                    }

                    _messenger.SendMessage(Properties.Resources.SuccessRegistration);

                    var win = (MainWindow)Application.Current.MainWindow;
                    win.Frame.Navigate(new Authentication());
                }));
            }
        }
    }
}