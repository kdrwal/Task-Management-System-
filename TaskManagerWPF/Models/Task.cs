using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;



namespace TaskManagerWPF.Models.Services;

public partial class Task
{
    [Key]
    public int TaskId { get; set; }

    public int? ProjectId { get; set; }

    [StringLength(100)]
    public string Title { get; set; }

    public string? Description { get; set; }

    //dodanie ProjectName
    public string? ProjectName => Project?.ProjectName;

    [StringLength(20)]
    public string? Priority { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [Column(TypeName = "bit")]
    public bool IsActive { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Task")]
    public virtual ICollection<File> Files { get; set; } = new List<File>();

    [ForeignKey("ProjectId")]
    [InverseProperty("Tasks")]
    public virtual Project? Project { get; set; }

    [InverseProperty("Task")]
    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskCategory> TaskCategories { get; set; } = new List<TaskCategory>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    [InverseProperty("Task")]
    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();

    public static implicit operator Task(ObservableCollection<Task> v)
    {
        throw new NotImplementedException();
    }
}
