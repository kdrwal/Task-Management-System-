using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class Comment
{
    [Key]
    public int CommentId { get; set; }

    public int? TaskId { get; set; }

    public int? UserId { get; set; }

    public string? Content { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("Comments")]
    public virtual Task? Task { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User? User { get; set; }
}
