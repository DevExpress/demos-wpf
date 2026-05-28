Imports DevExpress.AIIntegration.Blazor.Chat
Imports DevExpress.AIIntegration.Blazor.Chat.WebView
Imports Markdig
Imports Microsoft.AspNetCore.Components

Namespace AIDemo

    Public Partial Class AIChatCustomizeAppearanceModule

        Private ReadOnly messageTemplate As RenderFragment(Of BlazorChatMessage) = Function(message) Sub(builder)
            builder.OpenComponent(Of AIChatCustomizeAppearanceMessageTemplate)(0)
            builder.AddAttribute(1, NameOf(AIChatCustomizeAppearanceMessageTemplate.Context), message)
            builder.CloseComponent()
        End Sub

        Public Sub New()
            InitializeComponent()
            WarningText = Properties.Resource.EndpointWarning
            UpdateContentFormat()
            UpdateMessageTemplate()
            AddHandler aiChatControl.MarkdownConvert, AddressOf MarkdownConvert
        End Sub

        Private Sub OnCustomMessageAppearanceChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            UpdateMessageTemplate()
        End Sub

        Private Sub OnRichFormattedResponsesChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            UpdateContentFormat()
        End Sub

        Private Sub UpdateContentFormat()
            aiChatControl.ContentFormat = If(richFormattedResponsesCheckEdit.IsChecked = True, ResponseContentFormat.Markdown, ResponseContentFormat.PlainText)
        End Sub

        Private Sub UpdateMessageTemplate()
            aiChatControl.MessageTemplate = If(customMessageAppearanceCheckEdit.IsChecked = True, messageTemplate, Nothing)
        End Sub

        Private Sub MarkdownConvert(ByVal sender As Object, ByVal e As AIChatControlMarkdownConvertEventArgs)
            Dim html As String = Markdown.ToHtml(e.MarkdownText)
            e.HtmlText = CType(html, MarkupString)
        End Sub
    End Class
End Namespace
