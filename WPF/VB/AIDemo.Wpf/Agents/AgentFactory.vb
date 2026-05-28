Imports System
Imports System.Net.Http
Imports AIDemo.Agents.Publishing
Imports AIDemo.Agents.Shakespearean
Imports AIDemo.Agents.TextProcessing
Imports DevExpress.AIIntegration.Agents
Imports DevExpress.AIIntegration.Chat
Imports Microsoft.Agents.AI
Imports Microsoft.Agents.AI.AGUI
Imports Microsoft.Agents.AI.Workflows
Imports Microsoft.Extensions.AI

Namespace AIDemo.Agents

    Public Class AgentFactory

        Private ReadOnly chatClient As IChatClient

        Const WeatherAgentInstructions As String = "You are a weather assistant that provides instant weather information." & Microsoft.VisualBasic.vbLf & Microsoft.VisualBasic.vbLf & "CRITICAL RULES:" & Microsoft.VisualBasic.vbLf & "1. When a user asks about weather, IMMEDIATELY call the GetWeather tool with the city name - do NOT ask for clarification." & Microsoft.VisualBasic.vbLf & "2. If the city name is ambiguous (e.g., 'Paris'), assume the most well-known location (Paris, France)." & Microsoft.VisualBasic.vbLf & "3. Extract the city name from the user's question even if it's informal (e.g., 'weather in london' → 'London')." & Microsoft.VisualBasic.vbLf & "4. NEVER ask follow-up questions like 'Which Paris?' or 'Can you provide more details?'" & Microsoft.VisualBasic.vbLf & "5. If the city cannot be determined, make your best guess based on context." & Microsoft.VisualBasic.vbLf & "6. After calling the tool, provide a brief, friendly summary like 'Here\'s the current weather for [City]!' - keep it SHORT." & Microsoft.VisualBasic.vbLf & "7. Do NOT repeat the raw weather data in your text response since a weather widget is rendered automatically from the tool result."

        Public Sub New(ByVal chatClient As IChatClient)
            Me.chatClient = chatClient
        End Sub

        Public Function CreateShakespeareanPoetAgenticWorkflow() As IChatResponseProvider
            Dim poemAgent = CreatePoemAgent()
            Dim shakespeareanAgent = CreateShakespeareanAgent()
            Dim workflow = New WorkflowBuilder(poemAgent).AddEdge(poemAgent, shakespeareanAgent).Build()
            Return workflow.AsAIAgent(name:="ShakespeareanPoetWorkflow").AsIChatResponseProvider()
        End Function

        Public Function CreateShakespeareanPoetWorkflow() As Workflow
            Dim poemAgent = CreatePoemAgent()
            Dim shakespeareanAgent = CreateShakespeareanAgent()
            Return New WorkflowBuilder(poemAgent).AddEdge(poemAgent, shakespeareanAgent).Build()
        End Function

        Public Function CreatePublishingGroupChatWorkflow() As Workflow
            Dim contentReviewer As ChatClientAgent = New ChatClientAgent(chatClient, "You are a content reviewer responsible for checking articles before publication. " & "Review the article quality and run a plagiarism check, then report results clearly.", "ContentReviewer", "Content reviewer who checks article quality and originality", {AIFunctionFactory.Create(CType(AddressOf ReviewContent, Func(Of String, String))), AIFunctionFactory.Create(CType(AddressOf RunPlagiarismCheck, Func(Of String, String)))})
            Dim publisher As ChatClientAgent = New ChatClientAgent(chatClient, "You are a publisher responsible for scheduling and publishing articles. " & "First schedule the publication date, then proceed to publish to the live channels. " & "Always confirm the schedule is in place before publishing.", "Publisher", "Publisher who schedules and publishes articles to live channels", {AIFunctionFactory.Create(CType(AddressOf SchedulePublication, Func(Of String, String, String))), New ApprovalRequiredAIFunction(AIFunctionFactory.Create(CType(AddressOf PublishToLive, Func(Of String, String, String))))})
            Dim manager As PublishingGroupChatManager = New PublishingGroupChatManager({contentReviewer, publisher}) With {.MaximumIterationCount = 4}
            Return AgentWorkflowBuilder.CreateGroupChatBuilderWith(Function(__) manager).AddParticipants({contentReviewer, publisher}).Build()
        End Function

        Public Shared Function CreateTextProcessingWorkflow() As Workflow
            Dim uppercase = New UppercaseExecutor()
            Dim reverse = New ReverseTextExecutor()
            Return New WorkflowBuilder(uppercase).AddEdge(uppercase, reverse).Build()
        End Function

        Public Function CreatePoemAgent() As ChatClientAgent
            Const instructions As String = "You are a creative poet AI. Your task is to generate a short, lyrical poem (5-7 lines) " & "from a few keywords provided by the user." & Microsoft.VisualBasic.vbLf & Microsoft.VisualBasic.vbLf & "Rules:" & Microsoft.VisualBasic.vbLf & "- The user will send 2-3 words separated by spaces or commas." & Microsoft.VisualBasic.vbLf & "- Weave ALL provided keywords into the poem naturally." & Microsoft.VisualBasic.vbLf & "- Use vivid imagery, optional rhyme, and pleasant rhythm." & Microsoft.VisualBasic.vbLf & "- Output ONLY the poem text - no titles, no explanations, no extra commentary." & Microsoft.VisualBasic.vbLf & "- In the beginning of your response always add [Response from the PoemAgent] and a new line."
            Return chatClient.AsAIAgent(name:="PoemAgent", instructions:=instructions)
        End Function

        Public Function CreateShakespeareanAgent() As ChatClientAgent
            Const instructions As String = "You are a literary AI that transforms text into the style of William Shakespeare." & Microsoft.VisualBasic.vbLf & Microsoft.VisualBasic.vbLf & "Rules:" & Microsoft.VisualBasic.vbLf & "- Before rewriting, call the GetShakespeareanDictionary tool to retrieve a list of " & "authentic Elizabethan words and their meanings." & Microsoft.VisualBasic.vbLf & "- Use as many words from the dictionary as naturally fit in the rewritten text." & Microsoft.VisualBasic.vbLf & "- Rewrite the provided text using Elizabethan English (thee, thou, hath, doth, etc.)." & Microsoft.VisualBasic.vbLf & "- Preserve the original meaning and imagery of the text." & Microsoft.VisualBasic.vbLf & "- Add dramatic flair, poetic cadence, and Shakespearean vocabulary." & Microsoft.VisualBasic.vbLf & "- Output ONLY the rewritten text - no titles, no explanations, no extra commentary." & Microsoft.VisualBasic.vbLf & "- In the beginning of your response always add [Response from the ShakespeareanAgent] and a new line."
            Return chatClient.AsAIAgent(name:="ShakespeareanAgent", instructions:=instructions, tools:={GetDictionaryTool()})
        End Function

        Public Function CreateWeatherAgent(ByVal weatherTool As AIFunction, ByVal name As String) As AIAgent
            Return chatClient.AsAIAgent(name:=name, instructions:=WeatherAgentInstructions, tools:={weatherTool})
        End Function

        Public Shared Function CreateAGUIWeatherResponseProvider(ByVal agUiEndpoint As String) As IChatResponseProvider
            Dim httpClient = New HttpClient With {.Timeout = TimeSpan.FromSeconds(60)}
            Return CreateAGUIWeatherResponseProvider(httpClient, agUiEndpoint)
        End Function

        Public Shared Function CreateAGUIWeatherResponseProvider(ByVal httpClient As HttpClient, ByVal agUiEndpoint As String) As IChatResponseProvider
            Dim aguiChatClient = New AGUIChatClient(httpClient, agUiEndpoint)
            Dim agent = aguiChatClient.AsAIAgent(name:="weather-client", description:="AG-UI Weather Client Agent", instructions:=Nothing, tools:=Array.Empty(Of AIFunction)())
            Return agent.AsIChatResponseProvider()
        End Function
    End Class
End Namespace
