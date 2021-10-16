using ProjectManager.UI.ViewModels;
using System.Windows.Controls;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Views.Pages
{
    public partial class MainPage : Page
    {
        public MainPage(User user)
        {
            InitializeComponent();
            DataContext = new MainPageViewModel(user);
        }
    }
}
