using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.ViewModels.Single;
using TaskManagerWPF.ViewModels.Many;
using TaskManagerWPF.ViewModels;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Threading;


namespace TaskManagerWPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand OpenTaskViewCommand => new BaseCommand(() => CreateView(new TaskListViewModel()));
        public ICommand OpenAddTaskViewCommand => new BaseCommand(() => CreateView(new AddTaskViewModel()));
        public ICommand OpenUsersViewCommand => new BaseCommand(() => CreateView(new UserListViewModel()));
        public ICommand OpenAddUserViewCommand => new BaseCommand(() => CreateView(new AddUserViewModel()));
        public ICommand OpenProjectViewCommand => new BaseCommand(() => CreateView(new ProjectListViewModel()));
        public ICommand OpenAddProjectViewCommnad => new BaseCommand(() => CreateView(new AddProjectViewModel()));
        public ICommand OpenTaskAssignmentCommand => new BaseCommand(() => CreateView(new TaskAssignmentViewModel()));
        public ICommand OpenTaskDemandCommand => new BaseCommand(() => CreateView(new TaskDemandViewModel()));
        public ICommand OpenUserProductivity => new BaseCommand(() => CreateView(new UserProductivityViewModel()));
        public ICommand OpenCategory => new BaseCommand(() => CreateView(new CategoryListViewModel()));
        public ICommand OpenTag => new BaseCommand(() => CreateView(new TagListViewModel()));
        

        private DateTime _CurrentDateTime;
        public DateTime CurrentDateTime
        {
            get => _CurrentDateTime;
            set
            {
                if (_CurrentDateTime != value)
                {
                    _CurrentDateTime = value;
                    OnPropertyChanged(() => CurrentDateTime); 
                }
            }
        }

        private DispatcherTimer _timer;


        public MainWindowViewModel()
        {
            CurrentDateTime = DateTime.Now;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => CurrentDateTime = DateTime.Now;
            _timer.Start();


            Commands = new(CreateCommands());
            Workspaces = new();
            Workspaces.CollectionChanged += OnWorkspacesChanged;
            WeakReferenceMessenger.Default.Register<OpenViewMessage>(this, (sender, message) => OpenView(message));
            
        }

        public ReadOnlyCollection<CommandViewModel> Commands { get; set; }

        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>()
            {
                new CommandViewModel("Show Tasks", OpenTaskViewCommand), //TaskListViewModel
                new CommandViewModel("Add New Task", OpenAddTaskViewCommand), //AddTaskViewModel
                new CommandViewModel("Show Users", OpenUsersViewCommand), //UserListViewModel
                new CommandViewModel("Add User", OpenAddUserViewCommand), //Add user
                new CommandViewModel("Assign Task", OpenTaskAssignmentCommand),
                new CommandViewModel("Show Projects", OpenProjectViewCommand),
                
                new CommandViewModel("Task Statistics", OpenTaskDemandCommand),
                new CommandViewModel("User Productivity", OpenUserProductivity),
                new CommandViewModel("Category", OpenCategory),
                new CommandViewModel("Tags", OpenTag),
                
            };

         
            
        }

        public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }

        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;

            if (e.OldItems != null)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }

        private void OnWorkspaceRequestClose(object sender, System.EventArgs e)
        {
            WorkspaceViewModel? workspace = sender as WorkspaceViewModel;
            if (workspace != null)
            {
                Workspaces.Remove(workspace);
            }
        }

        private void OpenView(OpenViewMessage message)
        {
            CreateView(message.ViewModelToBeOpened);
        }



        private void CreateView(WorkspaceViewModel workspace)
        {
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        private void CreateListView<WorkspaceViewModelType>() where WorkspaceViewModelType : WorkspaceViewModel, new()
        {
            WorkspaceViewModel? workspace = Workspaces.FirstOrDefault(vm => vm is WorkspaceViewModelType);
            if (workspace == null)
            {
                workspace = new WorkspaceViewModelType();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        private void HandleMessage(string message)
        {
            Debug.WriteLine(message);
        }
    }
}