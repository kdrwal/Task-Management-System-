using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.Models.Contexts;
using TaskEntity = TaskManagerWPF.Models.Services.Task;
using Microsoft.EntityFrameworkCore;
using TaskManagerWPF.ViewModels.Many;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.Models.Dtos;
using Task = TaskManagerWPF.Models.Services.Task;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagerWPF.ViewModels.Single;
using CommunityToolkit.Mvvm.Input;

namespace TaskManagerWPF.ViewModels
{
    /// <summary>
    /// TaskListViewModel handles the logic for displaying and intracting with a list of TaksDto obejcts,
    /// allows to searching, deleting and marking them as completed
    /// </summary>
    public class TaskListViewModel : BaseManyViewModel<TaskService, TaskDto, Task>
    {

        private TaskDto _selectedModel;
        public TaskDto SelectedModel
        {
            get => _selectedModel;
            set
            {
                if (_selectedModel != value)
                {
                    _selectedModel = value;
                    OnPropertyChanged(() => SelectedModel);
                    //Update the command state for MarkAsCompleted
                    ((RelayCommand)MarkAsCompletedCommand)?.NotifyCanExecuteChanged();
                }
            }
        }
    
        //Commands
        public ICommand MarkAsCompletedCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        //Constructor
        public TaskListViewModel() : base("Task List")
        {
            MarkAsCompletedCommand = new RelayCommand(MarkSelectedTaskAsCompleted, CanMarkSelectedTaskAsCompleted);
            DeleteCommand = new RelayCommand(Delete);
        }
        //Determines if the currently celected Task can be marked as done
        private bool CanMarkSelectedTaskAsCompleted()
        {
            return SelectedModel != null && SelectedModel.Status != "Done";
        }
        //call the service to set the selected task's status to done
        private void MarkSelectedTaskAsCompleted()
        {
            if (SelectedModel != null)
            {
                Service.MarkTaskAsCompleted(SelectedModel);
               
               
            }
        }
        //clears searching filters
        protected override void ClearFilters()
        {
            SearchInput = null;
            SearchProperty = null;                  
        }
        //opens a view for creating a task
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddTaskViewModel()
            });
        }
        //deletes selected task by calling the service
        private void Delete()
        {
            if (SelectedModel != null)
            {
                Service.DeleteModel(SelectedModel);
            }
        }  
        
    }
}
