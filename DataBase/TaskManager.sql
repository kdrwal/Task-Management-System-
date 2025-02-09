CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME
);

CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME
);

CREATE TABLE UserRoles (
    UserRoleId INT PRIMARY KEY IDENTITY,
    UserId INT,
    RoleId INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE Projects (
    ProjectId INT PRIMARY KEY IDENTITY,
    ProjectName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME
);

CREATE TABLE Tasks (
    TaskId INT PRIMARY KEY IDENTITY,
    ProjectId INT,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Priority NVARCHAR(20) CHECK (Priority IN ('Low', 'Medium', 'High')),
    Status NVARCHAR(20) CHECK (Status IN ('To Do', 'In Progress', 'Done')),
    DueDate DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId)
);

CREATE TABLE TaskAssignments (
    TaskAssignmentId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    UserId INT,
    AssignedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME
);

CREATE TABLE TaskCategories (
    TaskCategoryId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    CategoryId INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE TABLE Tags (
    TagId INT PRIMARY KEY IDENTITY,
    TagName NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME
);

CREATE TABLE TaskTags (
    TaskTagId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    TagId INT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (TagId) REFERENCES Tags(TagId)
);

CREATE TABLE Comments (
    CommentId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    UserId INT,
    Content NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE TaskHistory (
    TaskHistoryId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    ChangedBy INT,
    ChangeDescription NVARCHAR(MAX),
    ChangedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (ChangedBy) REFERENCES Users(UserId)
);

CREATE TABLE Notifications (
    NotificationId INT PRIMARY KEY IDENTITY,
    UserId INT,
    Message NVARCHAR(MAX),
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Files (
    FileId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    FileName NVARCHAR(255),
    FilePath NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId)
);

CREATE TABLE SystemLogs (
    LogId INT PRIMARY KEY IDENTITY,
    UserId INT,
    Action NVARCHAR(255),
    Timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Reminders (
    ReminderId INT PRIMARY KEY IDENTITY,
    TaskId INT,
    UserId INT,
    ReminderDate DATETIME NOT NULL,
    IsSent BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    DeletedAt DATETIME,
    FOREIGN KEY (TaskId) REFERENCES Tasks(TaskId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);