using System.Collections.ObjectModel;
using Ninject;
using ProjectManager.BL.DTO;
using ProjectManager.BL.Interfaces;
using ProjectManager.UI.Common;
using ProjectManager.UI.Views;

namespace ProjectManager.UI.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        private UserDto _user;

        private IProjectService _projectService;
        private ITaskService _taskService;
        private IMessenger _messenger;

        private RelayCommand _addTask, _updateTask, _removeTask, _addProject, _updateProject, _deleteProject;
        private ObservableCollection<ProjectDto> _projects;
        private ProjectDto _currentProject, _selectedProject;
        private TaskDto _selectedTask;

        public MainPageViewModel(UserDto user)
        {
            _projectService = App.Container.Get<IProjectService>();
            _taskService = App.Container.Get<ITaskService>();
            _messenger = App.Container.Get<IMessenger>();
            _user = user;
            Projects = _user.Projects;
        }

        public ObservableCollection<ProjectDto> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        public ProjectDto CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                OnPropertyChanged(nameof(CurrentProject));  
            }
        }

        public ProjectDto SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        public TaskDto SelectedTask
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
                return _addProject ?? (_addProject = new RelayCommand(async _ =>
                {
                    var p = new ProjectDto();
                    var projectSettings = new ProjectSettingsWindow(p);
                    if(projectSettings.ShowDialog() == true)
                    {
                        p.UserId = _user.Id;
                        await _projectService.CreateProjectAsync(p);
                        Projects = new ObservableCollection<ProjectDto>(await _projectService.GetByUserIdAsync(_user.Id));
                    }
                }));
            }
        }

        public RelayCommand UpdateProjectCommand
        {
            get
            {
                return _updateProject ?? (_updateProject = new RelayCommand(async _ =>
                {
                    if (SelectedProject == null)
                    {
                        _messenger.SendMessage(Properties.Resources.ChooseRecord);
                        return;
                    }

                    var projectSettings = new ProjectSettingsWindow(SelectedProject);
                    projectSettings.ShowDialog();
                    if (projectSettings.DialogResult == true)
                       await _projectService.UpdateProjectAsync(SelectedProject);
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

                    if (_messenger.SendConfirmMessage(Properties.Resources.DelRecordConfirm))
                    {
                        await _projectService.DeleteProjectAsync(SelectedProject);
                        Projects.Remove(SelectedProject);
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

                        var t = new TaskDto();
                        var taskSettings = new TaskSettingsWindow(t);

                        if(taskSettings.ShowDialog() == true) 
                        {
                            t.ProjectId = CurrentProject.Id;
                            await _taskService.CreateTaskAsync(t);
                            CurrentProject.Tasks =
                            new ObservableCollection<TaskDto>
                            (await _taskService.GetTasksAsync(ts => ts.ProjectId == CurrentProject.Id));
                        } 
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

                    var taskSettings = new TaskSettingsWindow(SelectedTask);
                    taskSettings.ShowDialog();
                    if (taskSettings.DialogResult == true)
                        _taskService.UpdateTaskAsync(SelectedTask);
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

                    if(_messenger.SendConfirmMessage(Properties.Resources.DelRecordConfirm))
                    {
                        await _taskService.DeleteTaskAsync(SelectedTask);
                        CurrentProject.Tasks.Remove(SelectedTask);
                    }   
                }));
            }
        }
    }
}