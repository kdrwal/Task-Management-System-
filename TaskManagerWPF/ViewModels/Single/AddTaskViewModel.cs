using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.Helpers;
using System.Collections.ObjectModel;
using TaskManagerWPF.Models.Dtos;
using CommunityToolkit.Mvvm.Messaging;

namespace TaskManagerWPF.ViewModels.Single
{
    /// <summary>
    /// AddTaskViewMOdel handles creating a new Task in the system and saves them to the database when button is invoked
    /// </summary>
    public class AddTaskViewModel : BaseCreateViewModel<TaskService, TaskDto, Models.Services.Task>
    {

        public DatabaseContext Database { get; set; }

        public ICommand SaveandCloseCommand { get; set; }

        public Models.Services.Task Model { get; set; }

        public string Title
        {
            get => Model.Title;
            set
            {
                if(Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged(() => Title);
                }
            }
        }

        //ComboxDto Status

        private ObservableCollection<ComboBoxDto> _Status;
        public ObservableCollection<ComboBoxDto> Status
        {
            get => _Status;
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    OnPropertyChanged(() => _Status);
                }
            }
        }
        
        private string? _SelectedStatus;
        public string? SelectedStatus
        {
            get => _SelectedStatus;
            set
            {
                if (_SelectedStatus != value)
                {
                    _SelectedStatus = value;
                    Model.Status = value;
                    OnPropertyChanged(() => _SelectedStatus);
                }
            }
        }


        public int? ProjectId
        {
            get => Model.ProjectId;
            set
            {
                if(Model.ProjectId != value)
                {
                    Model.ProjectId = value;
                    OnPropertyChanged(() => ProjectId);
                }
            }
        }
        //ComboBox ProjectName
        private ObservableCollection<Project> _Projects;
        public ObservableCollection<Project> Projects
        {
            get => _Projects;
            set
            {
                if (_Projects != value)
                {
                    _Projects = value;
                    OnPropertyChanged(() => Projects);
                }
            }
        }

        private Project? _SelectedProject;
        public Project? SelectedProject
        {
            get => _SelectedProject;
            set
            {
                if (_SelectedProject != value)
                {
                    _SelectedProject = value;
                    Model.ProjectId = value?.ProjectId;
                    OnPropertyChanged(() => SelectedProject);
                }
            }
        }

        //ComboBoxDto Priority
        private ObservableCollection<ComboBoxDto> _Priority;

        public ObservableCollection<ComboBoxDto> Priority
        {
            get => _Priority;
            set
            {
                if (_Priority != value)
                {
                    _Priority = value;
                    OnPropertyChanged(() => Priority);
                }
            }
        }

        private string _SelectedPriority;
        public string SelectedPriority
        {
            get => _SelectedPriority;
            set
            {
                if (_SelectedPriority != value)
                {
                    _SelectedPriority = value;
                    Model.Priority = value; 
                    OnPropertyChanged(() => SelectedPriority);
                }
            }
        }


        public DateTime? DueDate
        {
            get => Model.DueDate;
            set
            {
                if (!Model.DueDate.HasValue)
                {
                    Model.DueDate = value;
                    OnPropertyChanged(() => DueDate);
                }
            }
        }

     



        public AddTaskViewModel() : base("Add new task") 
        {
            Database = new DatabaseContext();
            Model = new Models.Services.Task()
            {
                IsActive = true,
                DueDate = DateTime.Now
            };
            
            
            Database.Tasks.Add(Model);
            SaveandCloseCommand = new BaseCommand(() => SaveAndClose());

            List<ComboBoxDto> priority = Database.Tasks.Where(item => item.IsActive)
                                                       .Select(item => new ComboBoxDto()
                                                       {
                                                           Priority = item.Priority
                                                       }).Distinct()
                                                       .ToList();
            Priority = new ObservableCollection<ComboBoxDto>(priority);

            List<ComboBoxDto> status = Database.Tasks.Where(item => item.IsActive)
                                                       .Select(item => new ComboBoxDto()
                                                       {
                                                           Status = item.Status
                                                       }).Distinct()
                                                       .ToList();
            Status = new ObservableCollection<ComboBoxDto>(status);

            List<Project> projects = Database.Projects.Where(p => p.DeletedAt == null).ToList();
            Projects = new ObservableCollection<Project>(projects);

        }

        private void SaveAndClose()
        {
            WeakReferenceMessenger.Default.Send<string>("Hello, I have sent the message");
            try
            {
                Database.SaveChanges();
                OnRequestClose();
            } 
            catch (Exception ex)
            {
                MessageBox.Show("Oooops... Something went wrong... \n" + ex.ToString(), "Error");
            }
            
        }
    
    }
}
