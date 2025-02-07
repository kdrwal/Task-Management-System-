using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TaskManagerWPF.Helpers; 
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.ViewModels.Many;

namespace TaskManagerWPF.ViewModels.Many
{
    public class CategoryListViewModel : BaseManyViewModel<CategoryService, CategoryDto, Category>
    {
        private ObservableCollection<CategoryDto> _categories;
        public ObservableCollection<CategoryDto> Categories
        {
            get => _categories;
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged(() => Categories);
                }
            }
        }

        private CategoryDto _selectedCategory;
        public CategoryDto SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(() => SelectedCategory);

                    
                    if (_selectedCategory != null)
                    {
                        Name = _selectedCategory.Name;
                    }
                }
            }
        }

        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

       
        public ICommand SaveCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand DeleteCommand { get; }

        public CategoryListViewModel() : base("Category Management")
        {
            SaveCommand = new BaseCommand(Save);
            NewCommand = new BaseCommand(New);
            DeleteCommand = new BaseCommand(Delete);

            LoadCategories();
        }

        private void LoadCategories()
        {
            var list = Service.GetModels();
            Categories = new ObservableCollection<CategoryDto>(list);
        }

        private void Save()
        {
            
            if (SelectedCategory == null || SelectedCategory.CategoryId == 0)
            {
                
                var newModel = Service.CreateModel();
                newModel.CategoryName = Name;
                Service.AddModel(newModel);
            }
            else
            {
               
                var model = Service.GetModel(SelectedCategory.CategoryId);
                model.CategoryName = Name;
                Service.UpdateModel(model);
            }

            
            LoadCategories();
            ClearForm();
        }

        private void New()
        {
            
            ClearForm();
        }

        private void Delete()
        {
            if (SelectedCategory != null && SelectedCategory.CategoryId != 0)
            {
                Service.DeleteModel(SelectedCategory);
                LoadCategories();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            SelectedCategory = null;
            Name = string.Empty;
        }

        
        protected override void ClearFilters() {  }
        protected override void CreateNew() {  }
    }
}
