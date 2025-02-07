using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Models.Dtos
{
    public class TaskAssignmentDto
    {
        public int TaskAssignmentId { get; set; }
        public int? TaskId { get; set; }
        public int? UserId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime? AssignedAt { get; set; }
    }
}
