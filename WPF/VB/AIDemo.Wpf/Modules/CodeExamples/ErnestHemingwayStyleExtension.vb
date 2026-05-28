Imports System
Imports DevExpress.AIIntegration.Extensions

Namespace AIDemo.Modules.CodeExamples

    Public Class ErnestHemingwayStyleExtension
        Inherits DevExpress.AIIntegration.Extensions.ProofreadExtension

        Public Sub New(ByVal serviceProvider As System.IServiceProvider)
            MyBase.New(serviceProvider)
        End Sub

        Protected Overrides Function GetSystemPrompt(ByVal request As DevExpress.AIIntegration.Extensions.ProofreadRequest) As String
            Return "Rewrite this text in the Ernest Hemingway style."
        End Function
    End Class
End Namespace
