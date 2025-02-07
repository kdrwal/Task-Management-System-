using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;

namespace TaskManagerWPF.Models.Services
{
    public class TagService : BaseService<TagDto, Tag>
    {
        public override void AddModel(Tag model)
        {
            DatabaseContext.Tags.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(TagDto model)
        {
            var tag = DatabaseContext.Tags.FirstOrDefault(t => t.TagId == model.TagId);
            if (tag != null)
            {
                tag.DeletedAt = DateTime.Now;
                DatabaseContext.SaveChanges();
            }
        }

        public override Tag GetModel(int id)
        {
            return DatabaseContext.Tags.First(t => t.TagId == id);
        }

        public override List<TagDto> GetModels()
        {
            return DatabaseContext.Tags
                .Where(t => t.DeletedAt == null)
                .Select(t => new TagDto
                {
                    TagId = t.TagId,
                    Name = t.TagName
                })
                .ToList();
        }

        public override Tag CreateModel()
        {
            return new Tag()
            {
                CreatedAt = DateTime.Now
            };
        }

        public override void UpdateModel(Tag model)
        {
            model.UpdatedAt = DateTime.Now;
            DatabaseContext.Tags.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override string ValidateProperty(string columnName, Tag model)
        {
            if (columnName == nameof(Tag.TagName))
            {
                if (string.IsNullOrEmpty(model.TagName))
                {
                    return "Tag name is required.";
                }
            }
            return string.Empty;
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>() { new SearchComboBoxDto() };
        }

        public List<TagDto> GetTagsForTask(int taskId)
        {
            return DatabaseContext.TaskTags
                .Where(tt => tt.TaskId == taskId && tt.Tag.DeletedAt == null)
                .Select(tt => new TagDto
                {
                    TagId = tt.Tag.TagId,
                    Name = tt.Tag.TagName
                })
                .Distinct()
                .ToList();
        }

        public void AssignTagToTask(int taskId, int tagId)
        {
            
            bool exists = DatabaseContext.TaskTags
                .Any(tt => tt.TaskId == taskId && tt.TagId == tagId);

            if (!exists)
            {
                DatabaseContext.TaskTags.Add(new TaskTag
                {
                    TaskId = taskId,
                    TagId = tagId
                });
                DatabaseContext.SaveChanges();
            }
        }

        public void RemoveTagFromTask(int taskId, int tagId)
        {
            var record = DatabaseContext.TaskTags
                .FirstOrDefault(tt => tt.TaskId == taskId && tt.TagId == tagId);
            if (record != null)
            {
                
                DatabaseContext.TaskTags.Remove(record);
                DatabaseContext.SaveChanges();
            }
        }
    }
}
