Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class GroupChatWorkflowModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(GroupChatWorkflowModule)

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
                Return {New PromptSuggestion("Publish 'AI Trends 2026'", "Review and publish an article", "Please review and publish the article titled 'AI Trends 2026' to the website and newsletter.", False), New PromptSuggestion("Publish breaking news", "Urgent publication", "We have a breaking news story titled 'Major Tech Breakthrough'. Review and publish it immediately across all channels.", False), New PromptSuggestion("Publish 'Top 10 Dev Tools'", "Publish a listicle article", "The article 'Top 10 Dev Tools of 2026' is ready. Run all checks and publish it to the website and social media.", False)}
            End Get
        End Property

        Protected Overrides ReadOnly Property Icon As String
            Get
                Return "&#x1F5DE;&#xFE0F;"
            End Get
        End Property

        Protected Overrides ReadOnly Property Title As String
            Get
                Return "Group Chat Workflow with Tool Calling Approval"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "This is a multi-agent group chat with a <strong>Content Reviewer</strong> and a <strong>Publisher</strong>." & Microsoft.VisualBasic.vbLf & "The Publisher agent's <em>PublishToLive</em> tool requires your approval before execution." & Microsoft.VisualBasic.vbLf & "Start by requesting an article publication."
            End Get
        End Property
    End Class
End Namespace
