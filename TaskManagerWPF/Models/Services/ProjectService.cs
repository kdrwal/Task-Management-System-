using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    public class ProjectService : BaseService<ProjectDto, Project>
    {
        public override void AddModel(Project model)
        {
            DatabaseContext.Projects.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override Project CreateModel()
        {
            return new Project()
            {
                CreatedAt = DateTime.Now
            };
        }

        public override void DeleteModel(ProjectDto dto)
        {
            var project = DatabaseContext.Projects.FirstOrDefault(p => p.ProjectId == dto.ProjectId);
            if (project != null)
            {
                project.DeletedAt = DateTime.Now;
                DatabaseContext.SaveChanges();
            }
        }

        public override Project GetModel(int id)
        {
            return DatabaseContext.Projects.FirstOrDefault(p => p.ProjectId == id);
        }

        public override List<ProjectDto> GetModels()
        {
            IQueryable<Project> project = DatabaseContext.Projects.Where(item => item.DeletedAt == null);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                if(SearchProperty == nameof(Project.ProjectName))
                {
                    project = project.Where(item => item.ProjectName.Contains(SearchInput));
                } 
                else if (SearchProperty == nameof(Project.Description))
                {
                    project = project.Where(item => item.Description.Contains(SearchInput));
                }

            }

            return project.Select(item => new ProjectDto
            {
                ProjectName = item.ProjectName,
                Description = item.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt   = DateTime.Now,
            }).ToList();

            
        }


        public override void UpdateModel(Project model)
        {
            DatabaseContext.Projects.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(ProjectDto.ProjectName),
                    DisplayName = "Project Name"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = "Description",
                    DisplayName = "Description"
                }
            };
        }
        public override string ValidateProperty(string columnName, Project model)
        {
            if (columnName == nameof(Project.ProjectName))
            {
                if (string.IsNullOrEmpty(model.ProjectName))
                {
                    return "Project name is required";
                }
            }
            if (columnName == nameof(Project.Description))
            {
                if (string.IsNullOrEmpty(model.Description))
                {
                    return "Description is required";
                }
                

            }
            return string.Empty;
        }
    }
}
