Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.AIIntegration
Imports Microsoft.Extensions.Logging
Imports Microsoft.Extensions.Logging.Abstractions

Namespace AIDemo.Modules.CodeExamples

    Public Partial Class ErrorLoggingAndHandling
        Inherits System.Windows.Controls.UserControl

        Private ReadOnly exceptionHandler As AIDemo.Modules.CodeExamples.AIExceptionHandler

        Private defaultAIContainer As DevExpress.AIIntegration.AIExtensionsContainerDefault

        Public Sub New()
            Me.InitializeComponent()
            Dim logger As Microsoft.Extensions.Logging.ILogger = Microsoft.Extensions.Logging.Abstractions.NullLogger(Of AIDemo.Modules.CodeExamples.AIExceptionHandler).Instance
            Me.exceptionHandler = New AIDemo.Modules.CodeExamples.AIExceptionHandler(logger)
            Me.defaultAIContainer = New DevExpress.AIIntegration.AIExtensionsContainerDefault()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.defaultAIContainer.RegisterAIExceptionHandler(Me.exceptionHandler)
        End Sub

        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.defaultAIContainer.UnregisterAIExceptionHandler()
        End Sub
    End Class
End Namespace
