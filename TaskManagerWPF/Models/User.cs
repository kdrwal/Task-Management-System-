using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TaskManagerWPF.Helpers;

namespace TaskManagerWPF.Models.Services;

[Index("Email", Name = "UQ__Users__A9D10534B6A497BD", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string? Username { get; set; } 

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(256)]
    public string? PasswordHash { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("User")]
    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    [InverseProperty("User")]
    public virtual ICollection<SystemLog> SystemLogs { get; set; } = new List<SystemLog>();

    [InverseProperty("User")]
    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    [InverseProperty("ChangedByNavigation")]
    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    [Column(TypeName = "bit")]
    public bool IsActive { get; set; }

    public int? RoleId { get; set; } //foreign key
    [ForeignKey("RoleId")]
    public  Role Role { get; set; } // Link to Roles table

}
