using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerWPF.Helpers;

namespace TaskManagerWPF.Models.Dtos
{
    public class PriorityComboBoxItemDto
    {
        public PriorityEnum SelectedPriorityOption { get; set; }
        public string OptionPriorityName { get; set; } = default!;
    }
}
