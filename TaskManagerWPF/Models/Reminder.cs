using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class Reminder
{
    [Key]
    public int ReminderId { get; set; }

    public int? TaskId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReminderDate { get; set; }

    public bool? IsSent { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("TaskId")]
    [InverseProperty("Reminders")]
    public virtual Task? Task { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reminders")]
    public virtual User? User { get; set; }
}
