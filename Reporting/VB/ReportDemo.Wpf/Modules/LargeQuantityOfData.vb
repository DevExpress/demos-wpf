Imports System
Imports System.Data.SQLite
Imports System.IO
Imports DevExpress.Data.Utils
Imports DevExpress.XtraPrinting.Caching
Imports DevExpress.XtraReports
Imports DevExpress.XtraReports.UI

Namespace ReportWpfDemo

    Public Class LargeQuantityOfData
        Inherits ReportModuleViewModelBase

        Private documentStorageHelper As DbDocumentStorageHelper

        Private ReadOnly Property FolderPath As String
            Get
                Return Path.Combine(SafeEnvironment.LocalApplicationData, "DevExpress\ReportCache")
            End Get
        End Property

        Public Overrides Sub Clean()
            MyBase.Clean()
            documentStorageHelper.Dispose()
        End Sub

        Protected Overrides Function CreateReport() As IReport
            documentStorageHelper = New DbDocumentStorageHelper(FolderPath)
            Dim storage As DocumentStorage = documentStorageHelper.CreateStorage()
            Dim report As XtraReport = New XtraReportsDemos.CachedDocumentSourceReport.Report()
            Return New CachedReportSource(report, storage)
        End Function

        Protected Overrides Function CanShowDesigner() As Boolean
            Return False
        End Function
    End Class

    Friend Class DbDocumentStorageHelper
        Implements IDisposable

        Const dbFileName As String = "ReportCache"

        Const dbFileExtension As String = ".db"

        Private path As String

        Public Sub New(ByVal path As String)
            Me.path = path
        End Sub

        Public Function CreateStorage() As DocumentStorage
            Clear()
            CreateDirectory()
            Dim dbFileName As String = GetDbFileName()
            SQLiteConnection.CreateFile(dbFileName)
            Return New DbDocumentStorage("XpoProvider=SQLite;Data Source=" & dbFileName & ";Version=3;")
        End Function

        Public Sub Clear()
            Call SQLiteConnection.ClearAllPools()
            If Directory.Exists(path) Then ClearFiles(path)
        End Sub

        Private Shared Sub ClearFiles(ByVal path As String)
            For Each file As String In Directory.GetFiles(path, dbFileName & "*")
                Try
                    IO.File.Delete(file)
                Catch
                End Try
            Next
        End Sub

        Private Function GetDbFileName() As String
            Dim dbFile As String
            Dim i As Integer = 0
            Do
                dbFile = IO.Path.Combine(path, dbFileName & Math.Min(Threading.Interlocked.Increment(i), i - 1) & dbFileExtension)
            Loop While File.Exists(dbFile)

            Return dbFile
        End Function

        Private Sub CreateDirectory()
            If Not Directory.Exists(path) Then Directory.CreateDirectory(path)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Clear()
        End Sub
    End Class
End Namespace
