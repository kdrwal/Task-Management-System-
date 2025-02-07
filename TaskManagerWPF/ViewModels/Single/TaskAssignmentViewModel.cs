using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.ViewModels.Single;
using Task = TaskManagerWPF.Models.Services.Task;

namespace TaskManagerWPF.ViewModels.Single
{
    public class TaskAssignmentViewModel : BaseCreateViewModel<TaskAssignmentService, TaskAssignmentDto, TaskAssignment>
    {
        private readonly TaskAssignmentService _assignmentService;

       
        public ObservableCollection<TaskAssignmentDto> TaskAssignments { get; set; }
        public ObservableCollection<Task> Tasks { get; set; }
        public ObservableCollection<User> Users { get; set; }

        
        private Task _selectedTask;
        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged(() => SelectedTask);
                    ((RelayCommand)AssignTaskCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(() => SelectedUser);
                    ((RelayCommand)AssignTaskCommand).NotifyCanExecuteChanged();
                }
            }
        }

        
        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                if (_searchInput != value)
                {
                    _searchInput = value;
                    OnPropertyChanged(() => SearchInput);
                }
            }
        }

        private string _searchProperty;
        public string SearchProperty
        {
            get => _searchProperty;
            set
            {
                if (_searchProperty != value)
                {
                    _searchProperty = value;
                    OnPropertyChanged(() => SearchProperty);
                }
            }
        }

        
        public ObservableCollection<SearchComboBoxDto> SearchComboBoxDto { get; set; }

        
        private TaskAssignmentDto _selectedTaskAssignment;
        public TaskAssignmentDto SelectedTaskAssignment
        {
            get => _selectedTaskAssignment;
            set
            {
                if (_selectedTaskAssignment != value)
                {
                    _selectedTaskAssignment = value;
                    OnPropertyChanged(() => SelectedTaskAssignment);
                    ((RelayCommand)DeleteCommand).NotifyCanExecuteChanged();
                }
            }
        }

        // Commands
        public ICommand AssignTaskCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ClearFiltersCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public TaskAssignmentViewModel() : base("Task Assignment")
        {
            _assignmentService = new TaskAssignmentService();
            TaskAssignments = new ObservableCollection<TaskAssignmentDto>(_assignmentService.GetModels());
            Tasks = new ObservableCollection<Task>(LoadTasks());
            Users = new ObservableCollection<User>(LoadUsers());

            
            SearchComboBoxDto = new ObservableCollection<SearchComboBoxDto>()
            {
                new SearchComboBoxDto() { PropertyTitle = "TaskTitle", DisplayName = "Task Title" },
                new SearchComboBoxDto() { PropertyTitle = "UserName", DisplayName = "User Name" }
            };

            AssignTaskCommand = new RelayCommand(AssignTask, CanAssignTask);
            FilterCommand = new RelayCommand(Filter);
            ClearFiltersCommand = new RelayCommand(ClearFilters);
            RefreshCommand = new RelayCommand(Refresh);
            DeleteCommand = new RelayCommand(DeleteTaskAssignment, CanDeleteTaskAssignment);
        }

        private bool CanAssignTask() => SelectedTask != null && SelectedUser != null;

        private void AssignTask()
        {
            var assignment = new TaskAssignment
            {
                TaskId = SelectedTask.TaskId,
                UserId = SelectedUser.UserId,
                AssignedAt = DateTime.Now,
            };

            _assignmentService.AddModel(assignment);
            Refresh();
        }

        private void Filter()
        {
            var filtered = _assignmentService.GetModels();

            if (!string.IsNullOrEmpty(SearchInput))
            {
                if (SearchProperty == "TaskTitle")
                    filtered = filtered.Where(a => a.TaskTitle.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                else if (SearchProperty == "UserName")
                    filtered = filtered.Where(a => a.UserName.IndexOf(SearchInput, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            TaskAssignments.Clear();
            foreach (var assignment in filtered)
            {
                TaskAssignments.Add(assignment);
            }
        }

        private void ClearFilters()
        {
            SearchInput = string.Empty;
            SearchProperty = string.Empty;
            Refresh();
        }

        private void Refresh()
        {
            TaskAssignments.Clear();
            foreach (var assignment in _assignmentService.GetModels())
            {
                TaskAssignments.Add(assignment);
            }
        }

        private bool CanDeleteTaskAssignment() => SelectedTaskAssignment != null;

        private void DeleteTaskAssignment()
        {
            _assignmentService.DeleteModel(SelectedTaskAssignment);
            Refresh();
        }

        
        private List<Task> LoadTasks()
        {
            using (var db = new DatabaseContext())
            {
                return db.Tasks.Where(t => t.DeletedAt == null).ToList();
            }
        }

        private List<User> LoadUsers()
        {
            using (var db = new DatabaseContext())
            {
                return db.Users.Where(u => u.DeletedAt == null).ToList();
            }
        }
    }
}
