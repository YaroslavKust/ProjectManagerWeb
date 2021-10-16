using Ninject;
using ProjectManager.UI.Util;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;
using System.Windows;
using ProjectManager.UI.Models;

namespace ProjectManager.UI
{
    public partial class App : Application
    {
        public static User User { get; set; }

        public static string Token { get; set; }

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
            //var unitModule = new UnitModule();

            _container = new StandardKernel(serviceModule);
            Container = _container;

            var window = _container.Get<MainWindow>();
            var a = new MainWindow();
            window.Frame.Navigate(new Authentication());
            Current.MainWindow = window;
        }
    }
}
