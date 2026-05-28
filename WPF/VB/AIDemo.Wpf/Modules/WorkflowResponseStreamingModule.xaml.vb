Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class WorkflowResponseStreamingModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(WorkflowResponseStreamingModule)

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
                Return "Workflow Response Streaming"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "This demo implements the same Shakespearean Poet pipeline" & Microsoft.VisualBasic.vbLf & "and uses a custom workflow provider implementation (WorkflowResponseProvider)." & Microsoft.VisualBasic.vbLf & "This provider streams events in real time. Enter <strong>2 or 3 keywords</strong> to get started."
            End Get
        End Property
    End Class
End Namespace
