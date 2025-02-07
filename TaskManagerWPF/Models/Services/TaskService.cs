using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    /// <summary>
    /// TaskService handles the databaase operations for the Task entity,
    /// working with TaskDto on the UI side and Task entity in the database
    /// It ineherits from BaseService which defines a standard structure
    /// for adding, deleting updatingm etc.
    /// </summary>
    public class TaskService : BaseService<TaskDto, Task>
    {

        //Adds a new Task entity to the database
        public override void AddModel(Task model)
        {
            DatabaseContext.Tasks.Add(model);
            DatabaseContext.SaveChanges();
        }
        //Marks the tasks as deleted, so it wont show up in normal queries
        public override void DeleteModel(TaskDto model)
        {
            var task = DatabaseContext.Tasks.FirstOrDefault(item => item.TaskId == model.TaskId);
            if (task != null)
            {
                task.DeletedAt = DateTime.Now;
                task.IsActive = false; 
                DatabaseContext.SaveChanges();
            }
        }
        //Retrieves a single Task entity by its Id
        public override Task GetModel(int id)
        {
            return DatabaseContext.Tasks.First(item => item.TaskId == id);
        }

        //Retrieves a list of TaskDto objects only actives ones, lso applies search filtering 
        public override List<TaskDto> GetModels()
        {
            IQueryable<Task> tasks = DatabaseContext.Tasks.Include(item => item.Project)
                                                          .Where(item => item.IsActive);

            if(!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Task.Title):
                        tasks = tasks.Where(item => item.Title.Contains(SearchInput));
                        break;
                    case nameof(Task.Priority):
                        tasks = tasks.Where(item => item.Priority.Contains(SearchInput));
                        break;
                    case nameof(Task.Status):
                        tasks = tasks.Where(item => item.Status.Contains(SearchInput));
                        break;
                }
            }
            IQueryable<TaskDto> tasksDto = tasks.Select(item => new TaskDto()
            {
                TaskId = item.TaskId,   
                Title = item.Title,
                ProjectName = item.ProjectName,
                Priority = item.Priority,
                DueDate = item.DueDate,
                Status = item.Status
            });
            return tasksDto.ToList();
        }
        //Creates a new Task object with default values 
        public override Task CreateModel()
        {
            return new Task()
            {
                IsActive = true,
                CreatedAt = DateTime.Now

            };
        }
        //Updates an exisitng Task in the database
        public override void UpdateModel(Task model)
        {
            DatabaseContext.Tasks.Update(model);
            DatabaseContext.SaveChanges();
        }
        //Returns a list of properties that can be used for searching
        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Task.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Task.Priority),
                    DisplayName = "Priority"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Task.Status),
                    DisplayName = "Status"
                },
            };
        }
        //Sets Task status to Done, if it is not already done
        public void MarkTaskAsCompleted(TaskDto dto)
        {
            //Retrieve the task by ID
            var task = GetModel(dto.TaskId);
            if (task != null && task.Status != "Done")
            {
                task.Status = "Done";
                task.UpdatedAt = DateTime.Now;
                UpdateModel(task);
            }
        }
        //Validates a property 
        public override string ValidateProperty(string columnName, Task model)
        {
            if(columnName == nameof(Task.Title))
            {
                if(string.IsNullOrEmpty(model.Title))
                {
                    return "Title is required";
                }
            }
            return string.Empty;
        }

        


    }
}
