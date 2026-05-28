Imports System.IO
Imports System.Windows
Imports System.Windows.Forms
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.Core
Imports DevExpress.XtraRichEdit.Services
Imports FormsApplication = System.Windows.Forms.Application

Namespace RichEditDemo

    Public Class DemoUtils

        Public Shared Function GetRelativePath(ByVal fileName As String) As String
            fileName = Path.Combine("Data", fileName)
            Return DataFilesHelper.GetRelativeFilePath(fileName)
        End Function

        Public Shared Sub ShowErrorMessage(ByVal message As String)
            ThemedMessageBox.Show(FormsApplication.ProductName, message, MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub
    End Class

    Friend NotInheritable Class UserAccountService
        Implements IUserAccountService

        Private Shared userName As String

        Private Function GetUserName() As String Implements IUserAccountService.GetUserName
            Return If(userName, Function()
                userName = GetCurrentUserName()
                Return userName
            End Function())
        End Function

        Private Function GetCurrentUserName() As String
            Try
                Return SystemInformation.UserName
            Catch
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
