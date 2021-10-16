using System.Windows;
using Ninject;
using ProjectManager.UI.Common;
using ProjectManager.UI.Models;
using ProjectManager.UI.Properties;
using ProjectManager.UI.Services;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;

namespace ProjectManager.UI.ViewModels
{
    public class CreateTaskViewModel
    {
        public MyTask Task { get; set; }

        private IMessenger _messenger;
        private ITaskService _taskService;

        private RelayCommand _create;

        public CreateTaskViewModel(int projectId)
        {
            _messenger = App.Container.Get<IMessenger>();
            _taskService = App.Container.Get<ITaskService>();
            Task = new MyTask(){ProjectId = projectId};
        }

        public RelayCommand CreateTaskCommand
        {
            get
            {
                return _create ?? (_create = new RelayCommand(async _ =>
                {
                    if (string.IsNullOrWhiteSpace(Task.Description))
                    {
                        _messenger.SendMessage(Resources.EmptyTaskDescription);
                        return;
                    }

                    var res = await _taskService.CreateAsync(Task);

                    if (res)
                    {
                        var window = Application.Current.MainWindow as MainWindow;
                        window?.Frame.Navigate(new MainPage(App.User));
                    }
                    else
                    {
                        _messenger.SendMessage("qwe");
                    }
                }));
            }
        }
    }
}