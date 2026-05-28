Imports System
Imports DevExpress.AIIntegration.Extensions

Namespace AIDemo.Modules.CodeExamples

    Public Class AuthoredStyleExtension
        Inherits DevExpress.AIIntegration.Extensions.ChangeTextExtension(Of AIDemo.Modules.CodeExamples.AuthoredStyleRequest)

        Public Sub New(ByVal serviceProvider As System.IServiceProvider)
            MyBase.New(serviceProvider)
        End Sub

        Protected Overrides Function GetSystemPrompt(ByVal request As AIDemo.Modules.CodeExamples.AuthoredStyleRequest) As String
            Return $"Rewrite this text in the {request.Author} style"
        End Function
    End Class
End Namespace
