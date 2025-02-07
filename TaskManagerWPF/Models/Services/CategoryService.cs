using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    public class CategoryService : BaseService<CategoryDto, Category>
    {
        public override void AddModel(Category model)
        {
            DatabaseContext.Categories.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(CategoryDto model)
        {
            var category = DatabaseContext.Categories.FirstOrDefault(c => c.CategoryId == model.CategoryId);
            if (category != null)
            {
                category.DeletedAt = DateTime.Now;  
                DatabaseContext.SaveChanges();
            }
        }

        public override Category GetModel(int id)
        {
            return DatabaseContext.Categories.First(c => c.CategoryId == id);
        }

        public override List<CategoryDto> GetModels()
        {
            
            return DatabaseContext.Categories
                .Where(c => c.DeletedAt == null)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.CategoryName
                })
                .ToList();
        }

        public override Category CreateModel()
        {
            return new Category()
            {
                CreatedAt = DateTime.Now
            };
        }

        public override void UpdateModel(Category model)
        {
            model.UpdatedAt = DateTime.Now;
            DatabaseContext.Categories.Update(model);
            DatabaseContext.SaveChanges();
        }

        
        public override string ValidateProperty(string columnName, Category model)
        {
            if (columnName == nameof(Category.CategoryName))
            {
                if (string.IsNullOrEmpty(model.CategoryName))
                {
                    return "Category name is required";
                }
            }
            return string.Empty;
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>() { new SearchComboBoxDto() };

        }
    }
}
