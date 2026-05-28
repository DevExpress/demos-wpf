Imports System
Imports System.Windows
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Extensions
Imports DevExpress.Xpf.Core

Namespace AIDemo

    Public Class AIExceptionHandler
        Implements IAIExceptionHandler

        Public Sub ProcessException(ByVal args As AIExceptionArgs) Implements IAIExceptionHandler.ProcessException
            If Not(TypeOf args.Exception Is AIDemoException) Then args.Exception = New Exception("Something went wrong.", args.Exception)
            If TypeOf args.AIExtension Is SmartSearchAIExtension OrElse TypeOf args.AIExtension Is SmartAutoCompleteExtension Then
                ThemedMessageBox.Show("AI Service Error", args.Exception.Message, MessageBoxButton.OK)
            End If
        End Sub
    End Class

    Public Class AIDemoException
        Inherits Exception

        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub
    End Class
End Namespace
