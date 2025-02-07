using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    
    public class UserProductivityService : BaseService<UserProductivityDto, UserProductivityDto>
    {
        public override void AddModel(UserProductivityDto model)
        {
            throw new NotImplementedException("");
        }

        public override UserProductivityDto CreateModel()
        {
            return new UserProductivityDto();
        }

        public override UserProductivityDto GetModel(int id)
        {
            throw new NotImplementedException("");
        }

        public override List<UserProductivityDto> GetModels()
        {
            using (var db = new DatabaseContext())
            {
                
                var users = db.Users.Where(u => u.DeletedAt == null).ToList();

                
                var results = users.Select(user =>
                {
                    
                    var assignmentList = db.TaskAssignments
                                           .Where(ta => ta.UserId == user.UserId && ta.DeletedAt == null)
                                           .ToList();
                    
                    var tasks = (from ta in assignmentList
                                 join t in db.Tasks on ta.TaskId equals t.TaskId
                                 where t.DeletedAt == null
                                 select t).ToList();

                    return new UserProductivityDto
                    {
                        Username = user.Username,
                        TasksAssigned = tasks.Count,
                        TasksCompleted = tasks.Count(t => t.Status == "Done"),
                        AvgCompletionTime = tasks.Where(t => t.Status == "Done" && t.CreatedAt != null && t.UpdatedAt != null)
                                                 .Select(t => (t.UpdatedAt.Value - t.CreatedAt.Value).TotalDays)
                                                 .DefaultIfEmpty(0)
                                                 .Average()
                    };
                }).ToList();

                return results;
            }
        }

        public override void UpdateModel(UserProductivityDto model)
        {
            throw new NotImplementedException("");
        }

        public override void DeleteModel(UserProductivityDto model)
        {
            throw new NotImplementedException("");
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }
    }
}
