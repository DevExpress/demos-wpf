Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class WorkflowWithExecutorsModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(WorkflowWithExecutorsModule)

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
                Return {New PromptSuggestion("Hello, World!", "Classic Greeting", "Hello, World!", False), New PromptSuggestion("DevExpress WPF", "Product Name", "DevExpress WPF", False), New PromptSuggestion("Workflow streaming rocks!", "Enthusiastic Message", "Workflow streaming rocks!", False)}
            End Get
        End Property

        Protected Overrides ReadOnly Property Icon As String
            Get
                Return "&#x1F504;"
            End Get
        End Property

        Protected Overrides ReadOnly Property Title As String
            Get
                Return "Workflow with Execution Steps"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "Enter any text and start a two-step transformation workflow:" & Microsoft.VisualBasic.vbLf & "<strong>Uppercase</strong> &rarr; <strong>Reverse</strong>." & Microsoft.VisualBasic.vbLf & "Our custom response provider (<em>WorkflowResponseProvider</em>) streams operation progress to the chat control in real time."
            End Get
        End Property
    End Class
End Namespace
