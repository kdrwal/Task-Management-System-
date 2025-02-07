using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWPF.Models.Services;

public partial class UserRole
{
    [Key]
    public int UserRoleId { get; set; }
    public string RoleName { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoles")]
    public virtual Role? Role { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User? User { get; set; }
}
