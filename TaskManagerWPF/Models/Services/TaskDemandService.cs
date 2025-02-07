using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    
    public class TaskDemandService : BaseService<TaskDemandDto, TaskDemandDto>
    {
        public override void AddModel(TaskDemandDto model)
        {
            throw new NotImplementedException("");
        }

        public override TaskDemandDto CreateModel()
        {
            return new TaskDemandDto();
        }

        public override TaskDemandDto GetModel(int id)
        {
            throw new NotImplementedException("");
        }

        public override List<TaskDemandDto> GetModels()
        {
            using (var db = new DatabaseContext())
            {
                DateTime startDate = DateTime.Now.AddYears(-1);
                var query = db.Tasks
                    .Where(t => t.CreatedAt != null && t.CreatedAt >= startDate && t.DeletedAt == null)
                    .GroupBy(t => new { Year = t.CreatedAt.Value.Year, Month = t.CreatedAt.Value.Month })
                    .AsEnumerable() // Switching to the client, so that further projection is performed in memory
                    .Select(g => new TaskDemandDto
                    {
                        Month = $"{g.Key.Year}-{g.Key.Month:00}",
                        TaskCount = g.Count()
                    })
                    .OrderBy(dto => dto.Month);
                return query.ToList();
            }
        }

        public override void UpdateModel(TaskDemandDto model)
        {
            throw new NotImplementedException("");
        }

        public override void DeleteModel(TaskDemandDto model)
        {
            throw new NotImplementedException("");
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }
    }
}
