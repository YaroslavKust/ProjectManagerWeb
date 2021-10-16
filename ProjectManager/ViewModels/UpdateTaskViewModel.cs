using System.Collections.Generic;
using System.Windows;
using Ninject;
using ProjectManager.UI.Common;
using ProjectManager.UI.Models;
using ProjectManager.UI.Properties;
using ProjectManager.UI.Services;
using ProjectManager.UI.Views.Pages;
using ProjectManager.UI.Views;

namespace ProjectManager.UI.ViewModels
{
    public class UpdateTaskViewModel
    {
        public MyTask Task { get; set; }
        public List<int> Progress { get; set; }

        private readonly IMessenger _messenger;
        private readonly ITaskService _taskService;

        private RelayCommand _update;

        public UpdateTaskViewModel(MyTask task)
        {
            _messenger = App.Container.Get<IMessenger>();
            _taskService = App.Container.Get<ITaskService>();

            Task = task;

            Progress = new List<int>();
            for (int i = 0; i <= 100; i += 10)
                Progress.Add(i);
        }

        public RelayCommand UpdateTaskCommand
        {
            get
            {
                return _update ?? (_update = new RelayCommand(async _ =>
                {
                    if (string.IsNullOrWhiteSpace(Task.Description))
                    {
                        _messenger.SendMessage(Resources.EmptyTaskDescription);
                        return;
                    }

                    var res = await _taskService.UpdateAsync(Task);

                    if (res)
                    { 
                        var window = Application.Current.MainWindow as MainWindow;
                        window?.Frame.Navigate(new MainPage(App.User));
                    }

                }));
            }
        }
    }
}