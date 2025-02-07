using TaskManagerWPF.Helpers;
using TaskManagerWPF.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.ViewModels.Many
{
   abstract public class BaseManyViewModel<ServiceType, DtoType, ModelType> 
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType: class
        where ModelType : new()
    {
        private ObservableCollection<DtoType> _Models;
        public ObservableCollection<DtoType> Models
        {
            get => _Models;
            set
            {
                if(_Models != value)
                {
                    _Models = value;
                    OnPropertyChanged(() => Models);
                }
            }
        }

        
        public List<PriorityComboBoxItemDto> PriorityOptions { get; set; }
        public List<SearchComboBoxDto> SearchComboBoxDto {  get; set; }

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CreateNewCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ClearFiltersCommand { get; set; }
        public string? SearchInput
        {
            get => Service.SearchInput;
            set
            {
                if(Service.SearchInput != value)
                {
                    Service.SearchInput = value;
                    OnPropertyChanged(() => SearchInput);
                    //Refresh();
                }
            }
        }

        public string? SearchProperty
        {
            get => Service.SearchProperty;
            set
            {
                if (Service.SearchProperty != value)
                {
                    Service.SearchProperty = value;
                    OnPropertyChanged(() => SearchProperty);
                    Refresh();
                }
            }
        }

        private DtoType? _SelectedModel;
        public DtoType? SelectedModel
        {
            get => _SelectedModel;
            set
            {
                if(_SelectedModel != value)
                {
                    _SelectedModel = value;
                    OnPropertyChanged(() => SelectedModel);
                }
            }
        }
        
        public BaseManyViewModel(string displayName) :base (displayName)
        {
            _Models = default!;
            Refresh();
            RefreshCommand = new BaseCommand(() => Refresh());
            DeleteCommand = new BaseCommand(() => Delete());
            CreateNewCommand = new BaseCommand(() => CreateNew());
            FilterCommand = new BaseCommand(() => Refresh());
            ClearFiltersCommand = new BaseCommand(() => ClearFilters());    
           

            PriorityOptions = new()
            {
                new PriorityComboBoxItemDto()
                {
                    SelectedPriorityOption = PriorityEnum.High,
                    OptionPriorityName = "High"
                },
                new PriorityComboBoxItemDto()
                {
                    SelectedPriorityOption = PriorityEnum.Medium,
                    OptionPriorityName = "Medium"
                },
                new PriorityComboBoxItemDto()
                {
                    SelectedPriorityOption = PriorityEnum.Low,
                    OptionPriorityName = "Low"
                }
            };

            SearchComboBoxDto = Service.GetSearchComboBoxDtos();
            
        }

        private void Refresh()
        {
            Models = new ObservableCollection<DtoType>(Service.GetModels());
        }

        private void Delete()
        {
            if(SelectedModel != null)
            {
                Service.DeleteModel(SelectedModel);
                Models.Remove(SelectedModel);
            }
        }

        protected abstract void ClearFilters(); 
        protected abstract void CreateNew();

        

    }
}
