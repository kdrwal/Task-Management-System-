using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.ViewModels.Single;
using TaskManagerWPF.ViewModels.Many;

namespace TaskManagerWPF.ViewModels.Many
{
    public class UserProductivityViewModel : BaseCreateViewModel<UserProductivityService, UserProductivityDto, UserProductivityDto>
    {
        public ObservableCollection<UserProductivityDto> ProductivityStats { get; set; }
        public ICommand RefreshCommand { get; set; }

        public UserProductivityViewModel() : base("User Productivity")
        {
            Service = new UserProductivityService();
            ProductivityStats = new ObservableCollection<UserProductivityDto>(Service.GetModels());
            RefreshCommand = new RelayCommand(Refresh);
        }

        private void Refresh()
        {
            ProductivityStats.Clear();
            foreach (var stat in Service.GetModels())
            {
                ProductivityStats.Add(stat);
            }
        }
    }
}
