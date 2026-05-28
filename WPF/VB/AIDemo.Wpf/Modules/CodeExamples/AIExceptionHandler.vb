Imports System
Imports DevExpress.AIIntegration
Imports Microsoft.Extensions.AI
Imports Microsoft.Extensions.Logging

Namespace AIDemo.Modules.CodeExamples

    Public Class AIExceptionHandler
        Implements DevExpress.AIIntegration.IAIExceptionHandler

        Private ReadOnly _logger As Microsoft.Extensions.Logging.ILogger

        Public Sub New(ByVal logger As Microsoft.Extensions.Logging.ILogger)
            Me._logger = logger
        End Sub

        Public Sub ProcessException(ByVal args As DevExpress.AIIntegration.AIExceptionArgs) Implements Global.DevExpress.AIIntegration.IAIExceptionHandler.ProcessException
            Me._logger.LogInformation("An error occurred: {ErrorMessage}", args.Exception.Message)
            args.Response = New Microsoft.Extensions.AI.ChatResponse(New Microsoft.Extensions.AI.ChatMessage(Microsoft.Extensions.AI.ChatRole.Assistant, "Something went wrong. Please try again later."))
            args.Exception = New System.Exception("Something went wrong. Please try again later.", args.Exception)
        End Sub
    End Class
End Namespace
