using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class Project
{
    [Key]
    public int ProjectId { get; set; }

    [StringLength(100)]
    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
