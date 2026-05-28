Imports System
Imports System.Collections.ObjectModel
Imports DevExpress.AIIntegration.Blazor.Chat
Imports DevExpress.AIIntegration.Blazor.Chat.WebView
Imports DevExpress.AIIntegration.Wpf.Chat
Imports DevExpress.Xpf.DemoBase
Imports Markdig
Imports Microsoft.AspNetCore.Components

Namespace AIDemo

    Public MustInherit Class AgentModuleBase
        Inherits DemoModule

        Private chatControlField As AIChatControl

        Protected ReadOnly Property ChatControl As AIChatControl
            Get
                Return chatControlField
            End Get
        End Property

        Protected MustOverride ReadOnly Property ChatClientServiceKey As String

        Protected MustOverride ReadOnly Property PromptSuggestions As PromptSuggestion()

        Protected MustOverride ReadOnly Property Icon As String

        Protected MustOverride ReadOnly Property Title As String

        Protected MustOverride ReadOnly Property Description As String

        Protected Sub InitializeAgentModule(ByVal moduleChatControl As AIChatControl)
            If moduleChatControl Is Nothing Then Throw New ArgumentNullException(NameOf(moduleChatControl))
            chatControlField = moduleChatControl
            WarningText = Properties.Resource.EndpointWarning
            chatControlField.ChatResponseProviderServiceKey = ChatClientServiceKey
            chatControlField.ContentFormat = ResponseContentFormat.Markdown
            AddHandler chatControlField.MarkdownConvert, AddressOf MarkdownConvert
            chatControlField.EmptyStateTemplate = Sub(builder)
                builder.OpenComponent(Of EmptyMessageAreaTemplate)(0)
                builder.AddAttribute(1, "Icon", Icon)
                builder.AddAttribute(2, "Title", Title)
                builder.AddAttribute(3, "Description", Description)
                builder.CloseComponent()
            End Sub
            chatControlField.PromptSuggestions = New ObservableCollection(Of IPromptSuggestion)(PromptSuggestions)
        End Sub

        Private Sub MarkdownConvert(ByVal sender As Object, ByVal e As AIChatControlMarkdownConvertEventArgs)
            Dim html As String = Markdown.ToHtml(e.MarkdownText)
            e.HtmlText = CType(html, MarkupString)
        End Sub
    End Class
End Namespace
