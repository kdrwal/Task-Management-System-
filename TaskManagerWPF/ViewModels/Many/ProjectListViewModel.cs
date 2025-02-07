using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.ViewModels.Single;

namespace TaskManagerWPF.ViewModels.Many
{
    public class ProjectListViewModel : BaseManyViewModel<ProjectService, ProjectDto, Project>
    {
        public ProjectListViewModel() : base("Project List")
        {
        }

        protected override void ClearFilters()
        {
            SearchInput = null;
            SearchProperty = null;
        }

        
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddProjectViewModel()
            });
        }
    }
}
