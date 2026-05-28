Imports System.IO
#If Not NET
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
#Else
Imports Microsoft.Data.Sqlite
Imports Microsoft.EntityFrameworkCore

#End If
Namespace SchedulingDemo

    Public Class AppointmentEntity

        Public Property Id As Integer

        Public Property AllDay As Boolean

        Public Property Subject As String

        Public Property Description As String

        Public Property Location As String

        Public Property Start As Date

        Public Property [End] As Date

        Public Property QueryStart As Date

        Public Property QueryEnd As Date

        Public Property AppointmentType As Integer

        Public Property RecurrenceInfo As String

        Public Property ReminderInfo As String

        Public Property ResourceId As Integer?

        Public Property Label As Integer

        Public Property Status As Integer

        Public Property TimeZoneId As String
    End Class

    Public Class ResourceEntity

        Public Property Id As Integer

        Public Property Description As String

        Public Property Group As String
    End Class

    Public Class SchedulingContext
        Inherits DbContext

        Const FileName As String = "scheduling.db"

        Public Shared Function IsExist() As Boolean
            Return File.Exists(FileName)
        End Function

#If Not NET
        static SchedulingContext() {
            Database.SetInitializer<SchedulingContext>(null);
        }
        public SchedulingContext() : base(CreateConnection(), true) { }
        static DbConnection CreateConnection() {
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection();
            connection.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = FileName }.ConnectionString;
            return connection;
        }
#Else
        Public Sub New()
            MyBase.New(CreateOptions())
        End Sub

        Private Shared Function CreateOptions() As DbContextOptions
            Dim connectionString = New SqliteConnectionStringBuilder With {.DataSource = FileName}.ConnectionString
            Dim options = SqliteDbContextOptionsBuilderExtensions.UseSqlite(New DbContextOptionsBuilder(Of SchedulingContext)(), connectionString).Options
            Return options
        End Function

#End If
        Public Property AppointmentEntities As DbSet(Of AppointmentEntity)

        Public Property ResourceEntities As DbSet(Of ResourceEntity)

        Public Sub SetAutoDetectChangesEnabled(ByVal value As Boolean)
#If Not NET
            Configuration.AutoDetectChangesEnabled = value;
#Else
            ChangeTracker.AutoDetectChangesEnabled = value
#End If
        End Sub

        Public Sub ExecuteSql(ByVal sql As String)
#If Not NET
            Database.ExecuteSqlCommand(sql);
#Else
            Database.ExecuteSqlRaw(sql)
#End If
        End Sub
    End Class

    Public Class AppointmentEntityHelper

        Public Shared Sub CopyProperties(ByVal source As AppointmentEntity, ByVal target As AppointmentEntity)
            target.AllDay = source.AllDay
            target.AppointmentType = source.AppointmentType
            target.Description = source.Description
            target.End = source.End
            target.Label = source.Label
            target.Location = source.Location
            target.QueryEnd = source.QueryEnd
            target.QueryStart = source.QueryStart
            target.RecurrenceInfo = source.RecurrenceInfo
            target.ReminderInfo = source.ReminderInfo
            target.ResourceId = source.ResourceId
            target.Start = source.Start
            target.Status = source.Status
            target.Subject = source.Subject
        End Sub
    End Class
End Namespace
