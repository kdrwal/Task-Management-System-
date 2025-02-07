using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class File
{
    [Key]
    public int FileId { get; set; }

    public int? TaskId { get; set; }

    [StringLength(255)]
    public string? FileName { get; set; }

    [StringLength(255)]
    public string? FilePath { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("Files")]
    public virtual Task? Task { get; set; }
}
