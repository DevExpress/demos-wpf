Imports DevExpress.AIIntegration.Blazor.Chat.WebView

Namespace AIDemo

    Public Partial Class ReasoningModule
        Inherits AgentModuleBase

        Public Const ServiceKey As String = NameOf(ReasoningModule)

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
                Return {New PromptSuggestion("Matrix Transposition", "Bash Scripting Challenge", "Write a bash script that accepts a string representation of a matrix (such as '[1,2],[3,4],[5,6]') and outputs the transpose in the same format.", False), New PromptSuggestion("Water Jug Problem", "Classic Logic Puzzle", "You have a 3-gallon jug and a 5-gallon jug. How can you measure exactly 4 gallons of water? Explain each step.", False), New PromptSuggestion("Probability Puzzle", "Math Reasoning", "A bag contains 3 red balls and 5 blue balls. You draw 2 balls without replacement. What is the probability that both balls are the same color? Show your work step by step.", False), New PromptSuggestion("Knights and Knaves", "Logical Deduction", "On an island, every person is either a knight (always tells the truth) or a knave (always lies). You meet two people, A and B. A says 'At least one of us is a knave.' What are A and B?", False)}
            End Get
        End Property

        Protected Overrides ReadOnly Property Icon As String
            Get
                Return "&#x1F9E0;"
            End Get
        End Property

        Protected Overrides ReadOnly Property Title As String
            Get
                Return "Responses API & Reasoning Models"
            End Get
        End Property

        Protected Overrides ReadOnly Property Description As String
            Get
                Return "Test the model's reasoning capabilities with logic puzzles, math problems," & Microsoft.VisualBasic.vbLf & "coding challenges, and other multi-step tasks." & Microsoft.VisualBasic.vbLf & "Choose a suggestion below or enter your own prompt."
            End Get
        End Property
    End Class
End Namespace
