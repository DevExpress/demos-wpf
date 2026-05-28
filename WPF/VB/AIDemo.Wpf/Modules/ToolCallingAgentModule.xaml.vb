Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class ToolCallingAgentModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(ToolCallingAgentModule)

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
                Return {New PromptSuggestion("Current UTC Time", "System Information", "What is the current UTC time?", False), New PromptSuggestion("Add Numbers", "Math Operation", "What is the sum of 42 and 58?", False), New PromptSuggestion("Reverse Text", "String Function", "Reverse the text 'Hello, World!'.", False)}
            End Get
        End Property

        Protected Overrides ReadOnly Property Icon As String
            Get
                Return "&#x1F527;"
            End Get
        End Property

        Protected Overrides ReadOnly Property Title As String
            Get
                Return "AI Agent with Tool Calling"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "This simple <strong>AI Agent</strong> is equipped with tools to retrieve the current time," & Microsoft.VisualBasic.vbLf & "add numbers, and reverse text." & Microsoft.VisualBasic.vbLf & "Choose a suggestion below or ask the agent to use one of its tools."
            End Get
        End Property
    End Class
End Namespace
