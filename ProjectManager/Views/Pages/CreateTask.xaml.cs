using System.Windows.Controls;
using ProjectManager.UI.ViewModels;

namespace ProjectManager.UI.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Page
    {
        public CreateTask(CreateTaskViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
