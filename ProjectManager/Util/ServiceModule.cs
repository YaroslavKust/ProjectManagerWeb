using Ninject.Modules;
using ProjectManager.UI.Common;
using ProjectManager.UI.Services;
using ITaskService = ProjectManager.UI.Services.ITaskService;
using ProjectService = ProjectManager.UI.Services.ProjectService;
using TaskService = ProjectManager.UI.Services.TaskService;


namespace ProjectManager.UI.Util
{
    public class ServiceModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<IAuthenticationService>().To<AuthenticationService>();
            Bind<IMessenger>().To<DefaultMessenger>();
            Bind<IConnector>().To<JsonConnector>();
        }
    }
}