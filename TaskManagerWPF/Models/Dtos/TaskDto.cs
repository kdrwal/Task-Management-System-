using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.Models.Services;

namespace TaskManagerWPF.Models.Dtos
{
    /// <summary>
    /// TaskDto is a Data transfer object that represents only the fields
    /// needed to display or manipulate a Task in the UI layer
    /// It simplfies data handling and prevents exposing the entire entity
    /// </summary>
    public class TaskDto
    {
        public int TaskId { get; set; }
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string? ProjectName {  get; set; }
        public string? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
    }
}
