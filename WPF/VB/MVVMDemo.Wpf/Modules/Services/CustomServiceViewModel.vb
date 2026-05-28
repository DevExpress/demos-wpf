Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System.Diagnostics
Imports System.IO
Imports System.Windows
Imports DevExpress.Data.Utils
Imports DevExpress.Security

Namespace MVVMDemo.Services

    Public Class CustomServiceViewModel

        Public Sub Export(ByVal format As ExportFormat)
            Dim path = SafePath.GetTempFileName(format.ToString().ToLower())
            Dim service As IExportService = GetRequiredService(Of IExportService)()
            Using file As FileStream = System.IO.File.Create(path)
                service.Export(file, format)
            End Using

            If MessageBox.Show("Do you want to open the following file?" & Microsoft.VisualBasic.vbCrLf & path, "Open File", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                SafeProcess.Start(path, Nothing, Sub(startInfo) startInfo.UseShellExecute = True)
            Else
                File.Delete(path)
            End If
        End Sub
    End Class

    Public Interface IExportService

        Sub Export(ByVal stream As Stream, ByVal format As ExportFormat)

    End Interface

    Public Enum ExportFormat
        Xlsx
        Pdf
    End Enum
End Namespace
