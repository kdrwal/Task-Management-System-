using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class TaskTag
{
    [Key]
    public int TaskTagId { get; set; }

    public int? TaskId { get; set; }

    public int? TagId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("TagId")]
    [InverseProperty("TaskTags")]
    public virtual Tag? Tag { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("TaskTags")]
    public virtual Task? Task { get; set; }
}
