using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Models.Dtos
{
    public class UserProductivityDto
    {
        public string Username { get; set; }
        public int TasksAssigned { get; set; }
        public int TasksCompleted { get; set; }
        public double AvgCompletionTime { get; set; } // in days
    }
}
