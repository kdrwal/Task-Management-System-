using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class SystemLog
{
    [Key]
    public int LogId { get; set; }

    public int? UserId { get; set; }

    [StringLength(255)]
    public string? Action { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Timestamp { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("SystemLogs")]
    public virtual User? User { get; set; }
}
