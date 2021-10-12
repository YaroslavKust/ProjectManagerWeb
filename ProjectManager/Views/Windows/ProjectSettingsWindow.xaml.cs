using ProjectManager.BL.DTO;
using System.Windows;

namespace ProjectManager.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ProjectSettingsWindow.xaml
    /// </summary>
    public partial class ProjectSettingsWindow : Window
    {
        public ProjectSettingsWindow(ProjectDto project)
        {
            InitializeComponent();
            DataContext = project;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
