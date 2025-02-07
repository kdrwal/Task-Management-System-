using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

[Table("TaskHistory")]
public partial class TaskHistory
{
    [Key]
    public int TaskHistoryId { get; set; }

    public int? TaskId { get; set; }

    public int? ChangedBy { get; set; }

    public string? ChangeDescription { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChangedAt { get; set; }

    [ForeignKey("ChangedBy")]
    [InverseProperty("TaskHistories")]
    public virtual User? ChangedByNavigation { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskHistories")]
    public virtual Task? Task { get; set; }
}
