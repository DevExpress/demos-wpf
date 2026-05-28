Imports System.Diagnostics
Imports System.IO
Imports System.Windows
Imports DevExpress.Data.Utils
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Security

Namespace MVVMDemo.Services

    Public Class UIObjectServiceViewModel

        Public Sub Export()
            Const format As String = "pdf"
            Dim path = SafePath.GetTempFileName(format)
            Dim service As IUIObjectService = GetRequiredService(Of IUIObjectService)("GridService")
            Using file As FileStream = System.IO.File.Create(path)
                service.Object.View.ExportToPdf(file)
            End Using

            If MessageBox.Show("Do you want to open the following file?" & Microsoft.VisualBasic.vbCrLf & path, "Open File", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                SafeProcess.Start(path, Nothing, Sub(startInfo) startInfo.UseShellExecute = True)
            Else
                File.Delete(path)
            End If
        End Sub

        Public Sub ShowColumnChooser()
            Dim service As IUIObjectService = GetRequiredService(Of IUIObjectService)("TableViewService")
            service.Object.ShowColumnChooser()
        End Sub
    End Class
End Namespace
