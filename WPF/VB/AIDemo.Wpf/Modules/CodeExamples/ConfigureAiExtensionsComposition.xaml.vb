Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Extensions

Namespace AIDemo.Modules.CodeExamples

    Public Partial Class ConfigureAiExtensionsComposition
        Inherits System.Windows.Controls.UserControl

        Private localContainer As DevExpress.AIIntegration.AIExtensionsContainerLocal = Nothing

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.localContainer = New DevExpress.AIIntegration.AIExtensionsContainerLocal(DevExpress.AIIntegration.AIExtensionsContainerDesktop.[Default])
            Me.localContainer.Register(Of DevExpress.AIIntegration.Extensions.ProofreadRequest, AIDemo.Modules.CodeExamples.ErnestHemingwayStyleExtension)()
            AddHandler DevExpress.AIIntegration.AIExtensionsContainerDesktop.QueryIAIExtensionsContainer, AddressOf Me.QueryAiExtensionsContainerHandler
        End Sub

        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            RemoveHandler DevExpress.AIIntegration.AIExtensionsContainerDesktop.QueryIAIExtensionsContainer, AddressOf Me.QueryAiExtensionsContainerHandler
            Me.localContainer = Nothing
        End Sub

        Private Sub QueryAiExtensionsContainerHandler(ByVal sender As Object, ByVal e As DevExpress.AIIntegration.AIExtensionsContainerQueryEventArgs)
            If e.Source Is Me.textEdit Then e.Container = Me.localContainer
        End Sub
    End Class
End Namespace
