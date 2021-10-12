using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Windows;
using Ninject;
using ProjectManager.BL.DTO;
using ProjectManager.BL.Infrastructure;
using ProjectManager.UI.Util;
using ProjectManager.UI.ViewModels;
using ProjectManager.UI.Views;

namespace ProjectManager.UI
{
    public partial class App : Application
    {
        private IKernel _container;
        public static IKernel Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {

            var serviceModule = new ServiceModule();
            var unitModule = new UnitModule();

            _container = new StandardKernel(serviceModule, unitModule);
            Container = _container;

            var window = _container.Get<MainWindow>();
            var a = new MainWindow();
            window.Frame.Source = new Uri("../Pages/Authentication.xaml", UriKind.Relative);
            Current.MainWindow = window;
        }
    }
}
