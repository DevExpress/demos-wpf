Imports DevExpress.AIIntegration.Blazor.Chat
Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class AGUIIntegrationModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(AGUIIntegrationModule)

        Public Sub New()
            InitializeComponent()
            InitializeAgentModule(aiChatControl)
            ChatControl.ContentFormat = ResponseContentFormat.PlainText
            ChatControl.UseStreaming = False
            ChatControl.MessageContentTemplate = Function(message) Sub(builder)
                builder.OpenComponent(Of WeatherMessageContentTemplate)(0)
                builder.AddAttribute(1, "Message", message)
                builder.CloseComponent()
            End Sub
        End Sub

        Protected Overrides ReadOnly Property ChatClientServiceKey As String
            Get
                Return ServiceKey
            End Get
        End Property

        Protected Overrides ReadOnly Property PromptSuggestions As PromptSuggestion()
            Get
                Return {New PromptSuggestion("Weather in London", "Check the weather in the UK capital", "What's the weather in London?", False), New PromptSuggestion("Weather in Tokyo", "Check the weather in Japan's capital", "How's the weather looking in Tokyo?", False), New PromptSuggestion("Weather in New York", "Check the weather in New York (the Big Apple)", "Tell me about the weather in New York.", False)}
            End Get
        End Property

        Protected Overrides ReadOnly Property Icon As String
            Get
                Return "&#x1F324;&#xFE0F;"
            End Get
        End Property

        Protected Overrides ReadOnly Property Title As String
            Get
                Return "AG-UI Weather Agent"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "Ask about the weather in any city. Pick a suggestion below or enter your own prompt." & Microsoft.VisualBasic.vbLf & "The chat control queries a self-hosted <strong>Weather Agent</strong>." & Microsoft.VisualBasic.vbLf & "<strong>AG-UI protocol</strong> is used for communication."
            End Get
        End Property
    End Class
End Namespace
