using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class TaskCategory
{
    [Key]
    public int TaskCategoryId { get; set; }

    public int? TaskId { get; set; }

    public int? CategoryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TaskCategories")]
    public virtual Category? Category { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskCategories")]
    public virtual Task? Task { get; set; }
}
