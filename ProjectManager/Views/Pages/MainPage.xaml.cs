using ProjectManager.UI.ViewModels;
using System.Windows.Controls;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Views.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }
    }
}
