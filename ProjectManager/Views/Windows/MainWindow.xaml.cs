using ProjectManager.UI.Views.Pages;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace ProjectManager.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
