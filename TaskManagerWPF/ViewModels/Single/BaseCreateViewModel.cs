
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.Helpers;
using System.ComponentModel;

namespace TaskManagerWPF.ViewModels.Single
{
    public class BaseCreateViewModel<ServiceType, DtoType, ModelType>
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>, IDataErrorInfo
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType : class
        where ModelType : class, new()
    {
        private ModelType _Model;
        public ModelType Model
        {
            get => _Model;
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnPropertyChanged(() => Model);
                }
            }

        }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Error => string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">Name of the control that we are validtating name used in Binding</param>
        /// <returns>string.empty if no erro, error or the error</returns>
        public string this[string columnName] 
        { 
            get
            {
                return Service.ValidateProperty(columnName, Model);
            }
            
        }
        public BaseCreateViewModel(string displayName) : base(displayName)
        {
            _Model = Service.CreateModel();
            SaveCommand = new BaseCommand(() => Save());
            CancelCommand = new BaseCommand(() => Cancel());
        }

        public virtual void Save()
        {
            Service.AddModel(Model);
            OnRequestClose();
        }

        public void Cancel()
        {
            OnRequestClose();
        }


    }
}
