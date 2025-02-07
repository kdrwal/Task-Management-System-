using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class TaskAssignment
{
    [Key]
    public int TaskAssignmentId { get; set; }

    public int? TaskId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AssignedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskAssignments")]
    public virtual Task? Task { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TaskAssignments")]
    public virtual User? User { get; set; }
}
