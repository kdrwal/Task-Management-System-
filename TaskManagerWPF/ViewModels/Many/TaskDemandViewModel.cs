using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.ViewModels.Single;

namespace TaskManagerWPF.ViewModels.Many
{
    public class TaskDemandViewModel : BaseCreateViewModel<TaskDemandService, TaskDemandDto, TaskDemandDto>
    {
        public ObservableCollection<TaskDemandDto> TaskDemandStats { get; set; }
        public ICommand RefreshCommand { get; set; }

        public TaskDemandViewModel() : base("Task Statistics")
        {
            Service = new TaskDemandService();
            TaskDemandStats = new ObservableCollection<TaskDemandDto>(Service.GetModels());
            RefreshCommand = new RelayCommand(Refresh);
        }

        private void Refresh()
        {
            TaskDemandStats.Clear();
            foreach (var item in Service.GetModels())
            {
                TaskDemandStats.Add(item);
            }
        }
    }
}
