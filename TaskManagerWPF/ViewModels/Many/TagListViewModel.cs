using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.ViewModels.Many;

namespace TaskManagerWPF.ViewModels.Many
{
    public class TagListViewModel : BaseManyViewModel<TagService, TagDto, Tag>
    {
        private ObservableCollection<TagDto> _tags;
        public ObservableCollection<TagDto> Tags
        {
            get => _tags;
            set
            {
                if (_tags != value)
                {
                    _tags = value;
                    OnPropertyChanged(() => Tags);
                }
            }
        }

        private TagDto _selectedTag;
        public TagDto SelectedTag
        {
            get => _selectedTag;
            set
            {
                if (_selectedTag != value)
                {
                    _selectedTag = value;
                    OnPropertyChanged(() => SelectedTag);

                    if (_selectedTag != null)
                    {
                        Name = _selectedTag.Name;
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

        public TagListViewModel() : base("Tag Management")
        {
            SaveCommand = new BaseCommand(Save);
            NewCommand = new BaseCommand(New);
            DeleteCommand = new BaseCommand(Delete);

            LoadTags();
        }

        private void LoadTags()
        {
            var list = Service.GetModels();
            Tags = new ObservableCollection<TagDto>(list);
        }

        private void Save()
        {
            if (SelectedTag == null || SelectedTag.TagId == 0)
            {
                var newTag = Service.CreateModel();
                newTag.TagName = Name;
                Service.AddModel(newTag);
            }
            else
            {
                var tag = Service.GetModel(SelectedTag.TagId);
                tag.TagName = Name;
                Service.UpdateModel(tag);
            }

            LoadTags();
            ClearForm();
        }

        private void New()
        {
            ClearForm();
        }

        private void Delete()
        {
            if (SelectedTag != null && SelectedTag.TagId != 0)
            {
                Service.DeleteModel(SelectedTag);
                LoadTags();
                ClearForm();
            }
        }

        private void ClearForm()
        {
            SelectedTag = null;
            Name = string.Empty;
        }

        protected override void ClearFilters() { }
        protected override void CreateNew() { }
    }
}
