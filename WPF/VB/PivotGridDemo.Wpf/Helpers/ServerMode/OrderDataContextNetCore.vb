Imports System.IO
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Builders
Imports DbModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder

Namespace PivotGridDemo

    Public Class OrderDataContext
        Inherits DbContext

        Public Shared ReadOnly FileName As String = Path.GetFullPath("OrderData.db")

        Public Property OrderDataRecords As DbSet(Of OrderDataRecord)

        Public Sub New()
            MyBase.New()
            Call SetFilePath()
            connectionString = String.Format("Data Source={0}", filePath)
        End Sub

        Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
            optionsBuilder.UseLazyLoadingProxies.UseSqlite(connectionString)
        End Sub

        Private connectionString As String

        Private Shared defaultContext As OrderDataContext

        Public Shared ReadOnly Property [Default] As OrderDataContext
            Get
                Return If(defaultContext, Function()
                    defaultContext = New OrderDataContext()
                    Return defaultContext
                End Function())
            End Get
        End Property

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.ApplyConfiguration(New OrderDataRecordMap())
        End Sub

        Private Shared filePath As String

        Private Shared Sub SetFilePath()
            If Equals(filePath, Nothing) Then filePath = FileName
            Try
                Dim attributes = File.GetAttributes(filePath)
                If attributes.HasFlag(FileAttributes.ReadOnly) Then
                    File.SetAttributes(filePath, attributes And Not FileAttributes.ReadOnly)
                End If
            Catch
            End Try
        End Sub
    End Class

    Public Class OrderDataRecord

        Public Property Id As Integer

        Public Property OrderDate As Date

        Public Property Quantity As Integer

        Public Property UnitPrice As Decimal

        Public Property CustomerName As String

        Public Property ProductName As String

        Public Property CategoryName As String

        Public Property SalesPersonName As String
    End Class

    Public Class ProductDataRecord

        Public Property ProductName As String

        Public Property CategoryName As String

        Public Property UnitPrice As Decimal
    End Class

    Public Class OrderDataRecordMap
        Implements IEntityTypeConfiguration(Of OrderDataRecord)

        Public Sub Configure(ByVal builder As EntityTypeBuilder(Of OrderDataRecord)) Implements IEntityTypeConfiguration(Of OrderDataRecord).Configure
            ' Primary Key
            builder.HasKey(Function(t) t.Id)
            ' Properties
            builder.[Property](Function(t) t.Quantity).HasColumnType("int")
            builder.[Property](Function(t) t.UnitPrice).HasConversion(Of Single)()
            builder.Property(Function(t) t.Id).ValueGeneratedNever()
            builder.Property(Function(t) t.OrderDate).ValueGeneratedNever()
            builder.Property(Function(t) t.Quantity).ValueGeneratedNever()
            builder.Property(Function(t) t.UnitPrice).ValueGeneratedNever()
            builder.Property(Function(t) t.CustomerName).HasMaxLength(100)
            builder.Property(Function(t) t.ProductName).HasMaxLength(100)
            builder.Property(Function(t) t.CategoryName).HasMaxLength(100)
            builder.Property(Function(t) t.SalesPersonName).HasMaxLength(100)
            builder.[Property](Function(e) e.UnitPrice).HasConversion(Of Double)()
            ' Table & Column Mappings
            builder.ToTable("OrderDataRecords")
            builder.[Property](Function(t) t.Id).HasColumnName("Id")
            builder.[Property](Function(t) t.OrderDate).HasColumnName("OrderDate")
            builder.[Property](Function(t) t.Quantity).HasColumnName("Quantity")
            builder.[Property](Function(t) t.UnitPrice).HasColumnName("UnitPrice")
            builder.[Property](Function(t) t.CustomerName).HasColumnName("CustomerName")
            builder.[Property](Function(t) t.ProductName).HasColumnName("ProductName")
            builder.[Property](Function(t) t.CategoryName).HasColumnName("CategoryName")
            builder.[Property](Function(t) t.SalesPersonName).HasColumnName("SalesPersonName")
        End Sub
    End Class
End Namespace
