using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    public class TaskAssignmentService : BaseService<TaskAssignmentDto, TaskAssignment>
    {
        public override void AddModel(TaskAssignment model)
        {
            
            DatabaseContext.TaskAssignments.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override TaskAssignment CreateModel()
        {
            return new TaskAssignment
            {
                AssignedAt = DateTime.Now
            };
        }

        public override void UpdateModel(TaskAssignment model)
        {
            DatabaseContext.TaskAssignments.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override TaskAssignment GetModel(int id)
        {
            return DatabaseContext.TaskAssignments
                .Include(a => a.Task)
                .Include(a => a.User)
                .FirstOrDefault(a => a.TaskAssignmentId == id);
        }

        public override List<TaskAssignmentDto> GetModels()
        {
            var query = DatabaseContext.TaskAssignments
                .Include(a => a.Task)
                .Include(a => a.User)
                .Where(a => a.DeletedAt == null);

            return query
                .Select(a => new TaskAssignmentDto
                {
                    TaskAssignmentId = a.TaskAssignmentId,
                    TaskId = a.TaskId,
                    UserId = a.UserId,
                    TaskTitle = a.Task != null ? a.Task.Title : "No task",
                    UserName = a.User != null ? a.User.Username : "No user",
                    AssignedAt = a.AssignedAt
                })
                .ToList();
        }

        
        public override void DeleteModel(TaskAssignmentDto model)
        {
            var assignment = DatabaseContext.TaskAssignments
                .FirstOrDefault(a => a.TaskAssignmentId == model.TaskAssignmentId);
            if (assignment != null)
            {
                assignment.DeletedAt = DateTime.Now;
                DatabaseContext.SaveChanges();
            }
        }

        
        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = "TaskTitle",
                    DisplayName = "Task Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = "UserName",
                    DisplayName = "User Name"
                }
            };
        }
    }
}
