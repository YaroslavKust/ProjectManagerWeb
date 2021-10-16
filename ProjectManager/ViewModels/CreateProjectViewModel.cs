using Ninject;
using ProjectManager.UI.Common;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;
using System.Windows;
using ProjectManager.UI.Models;
using ProjectManager.UI.Properties;
using ProjectManager.UI.Services;

namespace ProjectManager.UI.ViewModels
{

    public class CreateProjectViewModel: BaseViewModel
    {
        private RelayCommand _addProject;

        private IProjectService _projectService;
        private IMessenger _messenger;
        public Project Project { get; set; }

        public CreateProjectViewModel(Project project)
        {
            _projectService = App.Container.Get<IProjectService>();
            _messenger = App.Container.Get<IMessenger>();
            Project = project;
        }

        public RelayCommand ActionProjectCommand
        {
            get
            {
                return _addProject ?? (_addProject = new RelayCommand(async _ =>
                {
                    if (string.IsNullOrWhiteSpace(Project.Name))
                    {
                        _messenger.SendMessage(Resources.EmptyProjectName);
                        return;
                    }

                    var res = await _projectService.CreateAsync(Project);

                    if (res)
                    {
                        var window = Application.Current.MainWindow as MainWindow;
                        window?.Frame.Navigate(new MainPage(App.User));
                    }
                    else
                    {
                        _messenger.SendMessage("Непредвиденная ошибка");
                    }
                    
                }));
            }
        }
    }
}