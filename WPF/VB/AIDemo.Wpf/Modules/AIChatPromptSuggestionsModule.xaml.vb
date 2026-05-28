Imports System.Collections.ObjectModel
Imports System.Windows
Imports DevExpress.AIIntegration.Blazor.Chat
Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class AIChatPromptSuggestionsModule

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            UpdatePromptSuggestions()
        End Sub

        Private Sub OnSendOnClickChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            UpdatePromptSuggestions()
        End Sub

        Private Sub UpdatePromptSuggestions()
            aiChatControl.PromptSuggestions = CreatePromptSuggestions(sendOnClickCheckEdit.IsChecked = True)
        End Sub

        Private Shared Function CreatePromptSuggestions(ByVal sendOnClick As Boolean) As ObservableCollection(Of IPromptSuggestion)
            Return New ObservableCollection(Of IPromptSuggestion) From {New PromptSuggestion("Tell me a joke", "Take a break and enjoy a quick laugh", "Tell me a joke", sendOnClick), New PromptSuggestion("Summarize text", "Extract a quick summary(main ideas)", "Summarize text", sendOnClick), New PromptSuggestion("Write an email", "Make your text look and sound professional", "Write an email", sendOnClick), New PromptSuggestion("Brainstorm ideas", "Get creative input for your tasks", "Brainstorm ideas", sendOnClick), New PromptSuggestion("Fix my writing", "Avoid spelling, grammar, and style errors", "Fix my writing", sendOnClick)}
        End Function
    End Class
End Namespace
