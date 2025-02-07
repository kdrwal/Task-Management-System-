using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;

namespace TaskManagerWPF.ViewModels.Single
{
    public class AddProjectViewModel : BaseCreateViewModel<ProjectService, ProjectDto, Project>
    {
        public ICommand SaveCommand { get; set; }

        public string ProjectName
        {
            get => Model.ProjectName;
            set
            {
                if (Model.ProjectName != value)
                {
                    Model.ProjectName = value;
                    OnPropertyChanged(() => ProjectName);
                }
            }
        }

        public string? Description
        {
            get => Model.Description;
            set
            {
                if (Model.Description != value)
                {
                    Model.Description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public AddProjectViewModel() : base("Add New Project")
        {
            
            Model = Service.CreateModel();
            SaveCommand = new RelayCommand(Save);
        }

        public override void Save()
        {
            try
            {
                Service.AddModel(Model);
                OnRequestClose();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
