Imports System.Windows
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Extensions

Namespace AIDemo.Modules.CodeExamples

    Public Partial Class ModifyDefaultExtension

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Call DevExpress.AIIntegration.AIExtensionsContainerDesktop.[Default].Register(Of DevExpress.AIIntegration.Extensions.ProofreadRequest, AIDemo.Modules.CodeExamples.WilliamShakespeareStyleExtension)()
        End Sub

        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Call DevExpress.AIIntegration.AIExtensionsContainerDesktop.[Default].Register(Of DevExpress.AIIntegration.Extensions.ProofreadRequest, DevExpress.AIIntegration.Extensions.ProofreadExtension)()
        End Sub
    End Class
End Namespace
