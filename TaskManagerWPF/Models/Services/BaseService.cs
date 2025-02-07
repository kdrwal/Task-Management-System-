using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DtoType">DTO to display on list</typeparam>
    /// <typeparam name="ModelType">Model to add/edit</typeparam>
    public abstract class BaseService<DtoType, ModelType>
        where ModelType : new()
    {
        protected DatabaseContext DatabaseContext { get; set; }
        public string? SearchInput { get; set; }
        public string? SearchProperty { get; set; }

        public BaseService()
        { 
            DatabaseContext = new DatabaseContext();
        }

        public abstract List<DtoType> GetModels();
        public abstract ModelType GetModel(int id);
        public abstract void AddModel(ModelType model);
        public abstract void UpdateModel(ModelType model);
        public abstract void DeleteModel(DtoType model);
        public abstract List<SearchComboBoxDto> GetSearchComboBoxDtos();
        
        public virtual ModelType CreateModel()
        {
            return new ModelType();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">Name of the control that we are validtating name used in Binding</param>
        /// <returns>string.empty if no erro, error or the error</returns>
        public virtual string ValidateProperty(string columnName, ModelType model)
        {
            return string.Empty;
        }
            
    }
}
