using System.Collections.ObjectModel;
using System.Windows;
using Ninject;
using ProjectManager.UI.Common;
using ProjectManager.UI.Models;
using ProjectManager.UI.Services;
using ProjectManager.UI.Views;
using ProjectManager.UI.Views.Pages;

namespace ProjectManager.UI.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        private MainWindow _window;

        private IProjectService _projectService;
        private ITaskService _taskService;
        private IMessenger _messenger;

        private RelayCommand _addTask, _updateTask, _removeTask, _addProject, _updateProject, _deleteProject;
        private ObservableCollection<Project> _projects;
        private Project _currentProject, _selectedProject;
        private MyTask _selectedTask;

        public MainPageViewModel()
        {
            _projectService = App.Container.Get<IProjectService>();
            _taskService = App.Container.Get<ITaskService>();
            _messenger = App.Container.Get<IMessenger>();
            _window = Application.Current.MainWindow as MainWindow;
            InitProj();
        }

        private async void InitProj()
        {
            Projects = new ObservableCollection<Project>(await _projectService.GetAllAsync());

            foreach (var proj in Projects)
            {
                var tasks = await _taskService.GetAllAsync(proj.Id);
                proj.Tasks = new ObservableCollection<MyTask>(tasks);
            }
        }

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        public Project CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                OnPropertyChanged(nameof(CurrentProject));  
            }
        }

        public Project SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        public MyTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public RelayCommand AddProjectCommand
        {
            get
            {
                return _addProject ?? (_addProject = new RelayCommand( _ =>
                {
                    var p = new Project();
                    var projectSettings = new ProjectSettings();
                    var settings = new CreateProjectViewModel(p);
                    projectSettings.DataContext = settings;

                    _window?.Frame.Navigate(projectSettings);
                }));
            }
        }

        public RelayCommand UpdateProjectCommand
        {
            get
            {
                return _updateProject ?? (_updateProject = new RelayCommand( _ =>
                {
                    if (SelectedProject == null)
                    {
                        _messenger.SendMessage(Properties.Resources.ChooseRecord);
                        return;
                    }

                    var projectSettings = new ProjectSettings();
                    var settings = new UpdateProjectViewModel(SelectedProject);
                    projectSettings.DataContext = settings;

                    _window?.Frame.Navigate(projectSettings);
                }
                ));
            }
        }

        public RelayCommand DeleteProjectCommand
        {
            get
            {
                return _deleteProject ?? (_deleteProject = new RelayCommand(async _ =>
                {
                    if (SelectedProject == null)
                    {
                        _messenger.SendMessage(Properties.Resources.ChooseRecord);
                        return;
                    }

                    if (await _messenger.SendConfirmMessageAsync(Properties.Resources.DelRecordConfirm))
                    {
                        var res = await _projectService.DeleteAsync(SelectedProject.Id);
                        if (res)
                        {
                            Projects.Remove(SelectedProject);
                        }
                        
                    }
                }));
            }
        }

        public RelayCommand AddTaskCommand
        {
            get
            {
                return _addTask ?? (_addTask = new RelayCommand(async _ =>
                    {
                        if (CurrentProject == null)
                        {
                            _messenger.SendMessage(Properties.Resources.ChooseRecord);
                            return;
                        }

                        var createTaskViewModel = new CreateTaskViewModel(CurrentProject.Id);

                        _window?.Frame.Navigate(new CreateTask(createTaskViewModel));
                    }
                ));
            }
        }

        public RelayCommand UpdateTaskCommand
        {
            get
            {
                return _updateTask ?? (_updateTask = new RelayCommand(_ =>
                {
                    if(SelectedTask == null)
                    {
                        _messenger.SendMessage(Properties.Resources.ChooseRecord);
                        return;
                    }

                    var updateTaskViewModel = new UpdateTaskViewModel(SelectedTask);

                    _window?.Frame.Navigate(new UpdateTask(updateTaskViewModel));
                }
                ));
            }
        }

        public RelayCommand RemoveTaskCommand
        {
            get
            {
                return _removeTask ?? (_removeTask = new RelayCommand(async _ =>
                {
                    if (SelectedTask == null)
                    {
                        _messenger.SendMessage(Properties.Resources.ChooseRecord);
                        return;
                    }

                    if(await _messenger.SendConfirmMessageAsync(Properties.Resources.DelRecordConfirm))
                    {
                        var res = await _taskService.DeleteAsync(SelectedTask.ProjectId, SelectedTask.Id);

                        if(res)
                            CurrentProject.Tasks.Remove(SelectedTask);
                    }   
                }));
            }
        }
    }
}