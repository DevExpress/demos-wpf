Imports DevExpress.AIIntegration.Extensions

Namespace AIDemo.Modules.CodeExamples

    Public Class AuthoredStyleRequest
        Inherits DevExpress.AIIntegration.Extensions.TextRequest

        Public Sub New(ByVal author As String, ByVal text As String)
            MyBase.New(text)
            Me.Author = author
        End Sub

        Public Property Author As String
    End Class
End Namespace
