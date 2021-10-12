using Ninject.Modules;
using ProjectManager.BL.Interfaces;
using ProjectManager.BL.Services;
using ProjectManager.UI.ViewModels;

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
        }
    }
}