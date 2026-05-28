Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class WorkflowAsAgentModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(WorkflowAsAgentModule)

        Public Sub New()
            InitializeComponent()
            InitializeAgentModule(aiChatControl)
        End Sub

        Protected Overrides ReadOnly Property ChatClientServiceKey As String
            Get
                Return ServiceKey
            End Get
        End Property

        Protected Overrides ReadOnly Property PromptSuggestions As PromptSuggestion()
            Get
                Return {New PromptSuggestion("Moon, whisper, ocean", "Tranquil & Mystical", "moon, whisper, ocean", False), New PromptSuggestion("Fire, dream, shadow", "Heroic & Hopeful", "fire, dream, shadow", False), New PromptSuggestion("Rose, thunder, silence", "Romantic & Contemplative", "rose, thunder, silence", False)}
            End Get
        End Property

        Protected Overrides ReadOnly Property Icon As String
            Get
                Return "&#x1F3AD;"
            End Get
        End Property

        Protected Overrides ReadOnly Property Title As String
            Get
                Return "Shakespearean Poet"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "The AI Assistant uses a few keywords to craft a poem" & Microsoft.VisualBasic.vbLf & "then rewrites it in the grand style of <em>William Shakespeare</em>." & Microsoft.VisualBasic.vbLf & "Choose a keyword combination below or enter <strong>2 or 3 custom keywords</strong>."
            End Get
        End Property
    End Class
End Namespace
