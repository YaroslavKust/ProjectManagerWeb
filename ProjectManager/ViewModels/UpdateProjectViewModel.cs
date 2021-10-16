using Ninject;
using ProjectManager.UI.Common;
using System.Windows;
using ProjectManager.UI.Models;
using ProjectManager.UI.Properties;
using ProjectManager.UI.Services;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;

namespace ProjectManager.UI.ViewModels
{
    class UpdateProjectViewModel
    {
        private RelayCommand _updateProject;

        private IProjectService _projectService;
        private IMessenger _messenger;
        public Project Project { get; set; }

        public UpdateProjectViewModel(Project project)
        {
            _projectService = App.Container.Get<IProjectService>();
            _messenger = App.Container.Get<IMessenger>();
            Project = project;
        }

        public RelayCommand ActionProjectCommand
        {
            get
            {
                return _updateProject ?? (_updateProject = new RelayCommand(async _ =>
                {
                    if (string.IsNullOrWhiteSpace(Project.Name))
                    {
                        _messenger.SendMessage(Resources.EmptyProjectName);
                        return;
                    }

                    var res = await _projectService.UpdateAsync(Project);

                    if (res)
                    {
                        var window = Application.Current.MainWindow as MainWindow;
                        window?.Frame.Navigate(new MainPage(App.User));
                    }
                    else
                    {
                        _messenger.SendMessage("error");
                    }
                    
                }));
            }
        }
    }
}
