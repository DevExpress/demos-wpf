Imports System
Imports DevExpress.AIIntegration.Extensions

Namespace AIDemo.Modules.CodeExamples

    Public Class WilliamShakespeareStyleExtension
        Inherits DevExpress.AIIntegration.Extensions.ProofreadExtension

        Public Sub New(ByVal serviceProvider As System.IServiceProvider)
            MyBase.New(serviceProvider)
        End Sub

        Protected Overrides Function GetSystemPrompt(ByVal request As DevExpress.AIIntegration.Extensions.ProofreadRequest) As String
            Return "Rewrite this text in the William Shakespeare style."
        End Function
    End Class
End Namespace
