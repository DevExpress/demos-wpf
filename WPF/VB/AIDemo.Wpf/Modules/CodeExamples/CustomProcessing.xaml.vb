Imports System
Imports System.Threading
Imports System.Windows
Imports System.Windows.Controls
Imports Azure.AI.OpenAI
Imports DevExpress.AIIntegration
Imports DevExpress.AIIntegration.Extensions
Imports DevExpress.Xpf.Core
Imports Microsoft.Extensions.AI

Namespace AIDemo.Modules.CodeExamples

    Public Partial Class CustomProcessing
        Inherits System.Windows.Controls.UserControl

        Private defaultAIContainer As DevExpress.AIIntegration.AIExtensionsContainerDefault

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OnLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.defaultAIContainer = New DevExpress.AIIntegration.AIExtensionsContainerDefault()
            Dim azureOpenAiEndpoint As String = "https://public-api.devexpress.com/demo-openai"
            Dim azureOpenAiKey As String = "DEMO"
            Dim deploymentName As String = "gpt-4o-mini"
            Dim azureOpenAiClient As Microsoft.Extensions.AI.IChatClient = New Azure.AI.OpenAI.AzureOpenAIClient(CType((New System.Uri(CStr((azureOpenAiEndpoint)))), System.Uri), CType((New System.ClientModel.ApiKeyCredential(CStr((azureOpenAiKey)))), System.ClientModel.ApiKeyCredential)).GetChatClient(CStr((deploymentName))).AsIChatClient()
            Me.defaultAIContainer.RegisterChatClient(azureOpenAiClient)
            Me.defaultAIContainer.Register(Of DevExpress.AIIntegration.Extensions.TranslateRequest, DevExpress.AIIntegration.Extensions.TranslateExtension)()
        End Sub

        Private Sub OnUnloaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.defaultAIContainer = Nothing
        End Sub

        Private Async Sub OnClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            ' have fun
            Dim request = New DevExpress.AIIntegration.Extensions.TranslateRequest("Have fun experimenting with this code", "Elvish")
            Dim response = Await Me.defaultAIContainer.TranslateAsync(request, System.Threading.CancellationToken.None)
            Dim translatedText As String = response.Response
            ' OK
            If response.IsCompleted Then
            ElseIf Not response.IsRestrictedOrFailed Then
                While response.IsContinuationRequired
                    Await response.ContinueAsync()
                    translatedText += response.Response
                End While ' Something unexpected happens
            Else
                Select Case response.Status
                    Case DevExpress.AIIntegration.Extensions.ResponseStatus.MaxTokenLimitExceeded, DevExpress.AIIntegration.Extensions.ResponseStatus.InputSizeLimitExceeded
                        translatedText = "The text you're trying to send within a request is too long and exceeds the limit."
                    Case DevExpress.AIIntegration.Extensions.ResponseStatus.ContentFiltered
                        translatedText = "Potentially harmful content was detected in your request."
                    Case DevExpress.AIIntegration.Extensions.ResponseStatus.[Error]
                        translatedText = "An error occurred while processing the request."
                End Select
            End If

            Call DevExpress.Xpf.Core.DXMessageBox.Show(translatedText, "Translation", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information)
        End Sub
    End Class
End Namespace
