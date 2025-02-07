using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManagerWPF.Models.Services;

namespace TaskManagerWPF.Models.Contexts;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Services.Task> Tasks { get; set; }

    public virtual DbSet<TaskAssignment> TaskAssignments { get; set; }

    public virtual DbSet<TaskCategory> TaskCategories { get; set; }

    public virtual DbSet<TaskHistory> TaskHistories { get; set; }

    public virtual DbSet<TaskTag> TaskTags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-65U0N5D\\MSSQLSERVER01;Integrated Security=True;Trust Server Certificate=True;Database=TaskManager");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BEA78ED95");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFCA09E22BC8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments).HasConstraintName("FK__Comments__TaskId__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasConstraintName("FK__Comments__UserId__628FA481");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.FileId).HasName("PK__Files__6F0F98BFDBF225BD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Task).WithMany(p => p.Files).HasConstraintName("FK__Files__TaskId__6FE99F9F");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E123BF3E0AF");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsRead).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications).HasConstraintName("FK__Notificat__UserI__6C190EBB");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABEF0A223CAE0");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Reminder__01A83087E3496C62");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsSent).HasDefaultValue(false);

            entity.HasOne(d => d.Task).WithMany(p => p.Reminders).HasConstraintName("FK__Reminders__TaskI__787EE5A0");

            entity.HasOne(d => d.User).WithMany(p => p.Reminders).HasConstraintName("FK__Reminders__UserI__797309D9");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AC06A6FF0");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__SystemLo__5E54864857FF012A");

            entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.SystemLogs).HasConstraintName("FK__SystemLog__UserI__73BA3083");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__657CF9ACDCFF3B0A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Services.Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B172E8E6D2");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks).HasConstraintName("FK__Tasks__ProjectId__49C3F6B7");
        });

        modelBuilder.Entity<TaskAssignment>(entity =>
        {
            entity.HasKey(e => e.TaskAssignmentId).HasName("PK__TaskAssi__75E8D23F0C59EF19");

            entity.Property(e => e.AssignedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskAssignments).HasConstraintName("FK__TaskAssig__TaskI__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.TaskAssignments).HasConstraintName("FK__TaskAssig__UserI__4E88ABD4");
        });

        modelBuilder.Entity<TaskCategory>(entity =>
        {
            entity.HasKey(e => e.TaskCategoryId).HasName("PK__TaskCate__FEB1EA2E2F979801");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.TaskCategories).HasConstraintName("FK__TaskCateg__Categ__5629CD9C");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskCategories).HasConstraintName("FK__TaskCateg__TaskI__5535A963");
        });

        modelBuilder.Entity<TaskHistory>(entity =>
        {
            entity.HasKey(e => e.TaskHistoryId).HasName("PK__TaskHist__2F15B73C4E52C711");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.TaskHistories).HasConstraintName("FK__TaskHisto__Chang__6754599E");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskHistories).HasConstraintName("FK__TaskHisto__TaskI__66603565");
        });

        modelBuilder.Entity<TaskTag>(entity =>
        {
            entity.HasKey(e => e.TaskTagId).HasName("PK__TaskTags__6DB2488F213FD8CE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Tag).WithMany(p => p.TaskTags).HasConstraintName("FK__TaskTags__TagId__5DCAEF64");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskTags).HasConstraintName("FK__TaskTags__TaskId__5CD6CB2B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C31C52A6E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A357D864376");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasConstraintName("FK__UserRoles__RoleI__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasConstraintName("FK__UserRoles__UserI__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
