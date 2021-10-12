using ProjectManager.BL.DTO;
using System.Collections.Generic;
using System.Windows;

namespace ProjectManager.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskSettingsWindow.xaml
    /// </summary>
    public partial class TaskSettingsWindow : Window
    {
        public List<int> Progress { get; set; }
        public TaskDto Task { get; set; }
        public TaskSettingsWindow(TaskDto task)
        {
            InitializeComponent();
            Task = task;
            DataContext = this;
            Progress = new List<int>();
            for(int i = 0; i <= 100; i += 10)
                Progress.Add(i);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
