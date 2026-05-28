Imports System.Collections.ObjectModel
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.IO
Imports DevExpress.DemoData.Models.Mapping
Imports DevExpress.Xpf.DemoCenterBase
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports DbModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.DataAnnotations

Namespace DevExpress.DemoData.Models

    Public Partial Class Department

        Public Property ID As Long

        Public Property Name As String
    End Class
End Namespace

Namespace DevExpress.DemoData

    Public Class IssueDataLoader
        Inherits DataLoaderBase

        Private itemsLoaded As Boolean = False

        Private projectsLoaded As Boolean = False

        Private usersLoaded As Boolean = False

        Private context As IssueListContext = New IssueListContext()

        Public ReadOnly Property Items As ObservableCollection(Of Item)
            Get
                LoadIfNeed(itemsLoaded, context.Items)
                Return context.Items.Local.ToObservableCollection()
            End Get
        End Property

        Public ReadOnly Property Projects As ObservableCollection(Of Project)
            Get
                LoadIfNeed(projectsLoaded, context.Projects)
                Return context.Projects.Local.ToObservableCollection()
            End Get
        End Property

        Public ReadOnly Property Users As ObservableCollection(Of User)
            Get
                LoadIfNeed(usersLoaded, context.Users)
                Return context.Users.Local.ToObservableCollection()
            End Get
        End Property
    End Class

    Public Enum IssueType
        Request
        Bug
    End Enum

    Friend Module IssuePriorityHelper

        Public Const CellMergingImagesPath As String = "pack://application:,,,/GridDemo;component/Images/CellMerging/"
    End Module

    Public Enum IssuePriority
        <Image(CellMergingImagesPath & "Low.svg")>
        Low
        <Image(CellMergingImagesPath & "Medium.svg")>
        Medium
        <Image(CellMergingImagesPath & "High.svg")>
        High
    End Enum

    Public Enum Status
        [New]
        Postponed
        Fixed
        Rejected
    End Enum
End Namespace

' Item.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class Item

        Public Property ID As Long

        Public Property Name As String

        Public Property Type As IssueType

        Public Property ProjectID As Long?

        Public Property Priority As IssuePriority?

        Public Property Status As Status?

        Public Property CreatorID As Long?

        Public Property CreatedDate As Date?

        Public Property OwnerID As Long?

        Public Property ModifiedDate As Date?

        Public Property FixedDate As Date?

        Public Property Description As String

        Public Property Resolution As String
    End Class
End Namespace

' mainContext.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class IssueListContext
        Inherits DbContext

        Public Sub New()
            MyBase.New()
            Call SetFilePath()
            If Not Equals(filePath, DemoRunner.DBFileFailedString) Then connectionString = String.Format("Data Source={0}", filePath)
        End Sub

        Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
            optionsBuilder.UseLazyLoadingProxies.UseSqlite(connectionString)
        End Sub

        Private connectionString As String

        Private Shared defaultContext As IssueListContext

        Public Shared ReadOnly Property [Default] As IssueListContext
            Get
                Return If(defaultContext, Function()
                    defaultContext = New IssueListContext()
                    Return defaultContext
                End Function())
            End Get
        End Property

        Public Property Departments As DbSet(Of Department)

        Public Property Items As DbSet(Of Item)

        Public Property Projects As DbSet(Of Project)

        Public Property ProjectTeams As DbSet(Of ProjectTeam)

        Public Property Schedulers As DbSet(Of Scheduler)

        Public Property Tasks As DbSet(Of Task)

        Public Property Users As DbSet(Of User)

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.ApplyConfiguration(New DepartmentMap())
            modelBuilder.ApplyConfiguration(New ItemMap())
            modelBuilder.ApplyConfiguration(New ProjectMap())
            modelBuilder.ApplyConfiguration(New ProjectTeamMap())
            modelBuilder.ApplyConfiguration(New SchedulerMap())
            modelBuilder.ApplyConfiguration(New TaskMap())
            modelBuilder.ApplyConfiguration(New UserMap())
        End Sub

        Private Shared filePath As String

        Private Shared Sub SetFilePath()
            If Equals(filePath, Nothing) Then filePath = DemoRunner.GetDBFileSafe("issue-list.db")
            Try
                Dim attributes = File.GetAttributes(filePath)
                If attributes.HasFlag(FileAttributes.ReadOnly) Then
                    File.SetAttributes(filePath, attributes And Not FileAttributes.ReadOnly)
                End If
            Catch
            End Try
        End Sub
    End Class
End Namespace

' Project.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class Project

        Public Property ID As Long

        Public Property Name As String

        Public Property ManagerID As Long?
    End Class
End Namespace

' ProjectTeam.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class ProjectTeam

        Public Property ID As Long

        Public Property ProjectID As Long?

        Public Property UserID As Long?

        Public Property [Function] As String
    End Class
End Namespace

' Scheduler.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class Scheduler

        Public Property ID As Long

        Public Property ProjectID As Long?

        Public Property UserID As Long?

        Public Property Sunday As Short?

        Public Property Monday As Short?

        Public Property Tuesday As Short?

        Public Property Wednesday As Short?

        Public Property Thursday As Short?

        Public Property Friday As Short?

        Public Property Saturday As Short?
    End Class
End Namespace

' Task.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class Task

        Public Property ID As Long

        Public Property Name As String

        Public Property ParentID As Long

        Public Property UserID As Long

        Public Property StartDate As Date?

        Public Property EndDate As Date?

        Public Property Done As Boolean

        Public Property Priority As Long?

        Public Property Complete As Double?
    End Class
End Namespace

' User.cs
Namespace DevExpress.DemoData.Models

    Public Partial Class User

        Public Property ID As Long

        Public Property FirstName As String

        Public Property MiddleName As String

        Public Property LastName As String

        Public Property Country As String

        Public Property PostalCode As String

        Public Property City As String

        Public Property Address As String

        Public Property Phone As String

        Public Property Fax As String

        Public Property Email As String

        Public Property HomePage As String

        Public Property DepartmentID As Long?

        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
    End Class
End Namespace

' DepartmentMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class DepartmentMap
        Implements IEntityTypeConfiguration(Of Department)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Department)) Implements IEntityTypeConfiguration(Of Department).Configure
            ' Primary Key
            builder.HasKey(Function(t) t.ID)
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            builder.Property(Function(t) t.Name).HasMaxLength(100)
            ' Table & Column Mappings
            builder.ToTable("Departments")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.Name).HasColumnName("Name")
        End Sub
    End Class
End Namespace

' ItemMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class ItemMap
        Implements IEntityTypeConfiguration(Of Item)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Item)) Implements IEntityTypeConfiguration(Of Item).Configure
            ' Primary Key
            builder.HasKey(Function(t) New With {t.ID})
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            builder.Property(Function(t) t.Name).HasMaxLength(50)
            builder.Property(Function(t) t.Description).HasMaxLength(2147483647)
            builder.Property(Function(t) t.Resolution).HasMaxLength(2147483647)
            ' Table & Column Mappings
            builder.ToTable("Items")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.Name).HasColumnName("Name")
            builder.[Property](Function(t) t.Type).HasColumnName("Type")
            builder.[Property](Function(t) t.ProjectID).HasColumnName("ProjectID")
            builder.[Property](Function(t) t.Priority).HasColumnName("Priority")
            builder.[Property](Function(t) t.Status).HasColumnName("Status")
            builder.[Property](Function(t) t.CreatorID).HasColumnName("CreatorID")
            builder.[Property](Function(t) t.CreatedDate).HasColumnName("CreatedDate")
            builder.[Property](Function(t) t.OwnerID).HasColumnName("OwnerID")
            builder.[Property](Function(t) t.ModifiedDate).HasColumnName("ModifiedDate")
            builder.[Property](Function(t) t.FixedDate).HasColumnName("FixedDate")
            builder.[Property](Function(t) t.Description).HasColumnName("Description")
            builder.[Property](Function(t) t.Resolution).HasColumnName("Resolution")
        End Sub
    End Class
End Namespace

' ProjectMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class ProjectMap
        Implements IEntityTypeConfiguration(Of Project)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Project)) Implements IEntityTypeConfiguration(Of Project).Configure
            ' Primary Key
            builder.HasKey(Function(t) t.ID)
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            builder.Property(Function(t) t.Name).HasMaxLength(100)
            ' Table & Column Mappings
            builder.ToTable("Projects")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.Name).HasColumnName("Name")
            builder.[Property](Function(t) t.ManagerID).HasColumnName("ManagerID")
        End Sub
    End Class
End Namespace

' ProjectTeamMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class ProjectTeamMap
        Implements IEntityTypeConfiguration(Of ProjectTeam)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of ProjectTeam)) Implements IEntityTypeConfiguration(Of ProjectTeam).Configure
            ' Primary Key
            builder.HasKey(Function(t) t.ID)
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            builder.Property(Function(t) t.Function).HasMaxLength(50)
            ' Table & Column Mappings
            builder.ToTable("ProjectTeam")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.ProjectID).HasColumnName("ProjectID")
            builder.[Property](Function(t) t.UserID).HasColumnName("UserID")
            builder.[Property](Function(t) t.Function).HasColumnName("Function")
        End Sub
    End Class
End Namespace

' SchedulerMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class SchedulerMap
        Implements IEntityTypeConfiguration(Of Scheduler)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Scheduler)) Implements IEntityTypeConfiguration(Of Scheduler).Configure
            ' Primary Key
            builder.HasKey(Function(t) t.ID)
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            ' Table & Column Mappings
            builder.ToTable("Scheduler")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.ProjectID).HasColumnName("ProjectID")
            builder.[Property](Function(t) t.UserID).HasColumnName("UserID")
            builder.[Property](Function(t) t.Sunday).HasColumnName("Sunday")
            builder.[Property](Function(t) t.Monday).HasColumnName("Monday")
            builder.[Property](Function(t) t.Tuesday).HasColumnName("Tuesday")
            builder.[Property](Function(t) t.Wednesday).HasColumnName("Wednesday")
            builder.[Property](Function(t) t.Thursday).HasColumnName("Thursday")
            builder.[Property](Function(t) t.Friday).HasColumnName("Friday")
            builder.[Property](Function(t) t.Saturday).HasColumnName("Saturday")
        End Sub
    End Class
End Namespace

' TaskMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class TaskMap
        Implements IEntityTypeConfiguration(Of Task)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of Task)) Implements IEntityTypeConfiguration(Of Task).Configure
            ' Primary Key
            builder.HasKey(Function(t) New With {t.ID, t.Name, t.ParentID, t.UserID, t.Done})
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            builder.Property(Function(t) t.Name).IsRequired().HasMaxLength(50)
            builder.Property(Function(t) t.ParentID).ValueGeneratedNever()
            builder.Property(Function(t) t.UserID).ValueGeneratedNever()
            ' Table & Column Mappings
            builder.ToTable("Tasks")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.Name).HasColumnName("Name")
            builder.[Property](Function(t) t.ParentID).HasColumnName("ParentID")
            builder.[Property](Function(t) t.UserID).HasColumnName("UserID")
            builder.[Property](Function(t) t.StartDate).HasColumnName("StartDate")
            builder.[Property](Function(t) t.EndDate).HasColumnName("EndDate")
            builder.[Property](Function(t) t.Done).HasColumnName("Done")
            builder.[Property](Function(t) t.Priority).HasColumnName("Priority")
            builder.[Property](Function(t) t.Complete).HasColumnName("Complete")
        End Sub
    End Class
End Namespace

' UserMap.cs
Namespace DevExpress.DemoData.Models.Mapping

    Public Class UserMap
        Implements IEntityTypeConfiguration(Of User)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of User)) Implements IEntityTypeConfiguration(Of User).Configure
            ' Primary Key
            builder.HasKey(Function(t) t.ID)
            ' Properties
            builder.Property(Function(t) t.ID).ValueGeneratedNever()
            builder.Property(Function(t) t.FirstName).HasMaxLength(25)
            builder.Property(Function(t) t.MiddleName).HasMaxLength(20)
            builder.Property(Function(t) t.LastName).HasMaxLength(25)
            builder.Property(Function(t) t.Country).HasMaxLength(15)
            builder.Property(Function(t) t.PostalCode).HasMaxLength(10)
            builder.Property(Function(t) t.City).HasMaxLength(15)
            builder.Property(Function(t) t.Address).HasMaxLength(60)
            builder.Property(Function(t) t.Phone).HasMaxLength(24)
            builder.Property(Function(t) t.Fax).HasMaxLength(24)
            builder.Property(Function(t) t.Email).HasMaxLength(50)
            builder.Property(Function(t) t.HomePage).HasMaxLength(50)
            ' Table & Column Mappings
            builder.ToTable("Users")
            builder.[Property](Function(t) t.ID).HasColumnName("ID")
            builder.[Property](Function(t) t.FirstName).HasColumnName("FirstName")
            builder.[Property](Function(t) t.MiddleName).HasColumnName("MiddleName")
            builder.[Property](Function(t) t.LastName).HasColumnName("LastName")
            builder.[Property](Function(t) t.Country).HasColumnName("Country")
            builder.[Property](Function(t) t.PostalCode).HasColumnName("PostalCode")
            builder.[Property](Function(t) t.City).HasColumnName("City")
            builder.[Property](Function(t) t.Address).HasColumnName("Address")
            builder.[Property](Function(t) t.Phone).HasColumnName("Phone")
            builder.[Property](Function(t) t.Fax).HasColumnName("Fax")
            builder.[Property](Function(t) t.Email).HasColumnName("Email")
            builder.[Property](Function(t) t.HomePage).HasColumnName("HomePage")
            builder.[Property](Function(t) t.DepartmentID).HasColumnName("DepartmentID")
        End Sub
    End Class
End Namespace
