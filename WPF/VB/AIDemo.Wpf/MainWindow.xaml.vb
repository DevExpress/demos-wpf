Imports System
Imports System.ClientModel.Primitives
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net.Http
Imports System.Reflection
Imports System.Threading
Imports System.Windows
Imports AIDemo.SemanticSearch
Imports Azure.AI.OpenAI
Imports DevExpress.AIIntegration
Imports DevExpress.Data.Internal
Imports DevExpress.Data.Utils
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers
Imports Microsoft.Extensions.AI
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.SemanticKernel.Connectors.InMemory
#If NET
Imports AIDemo.Agents
Imports AIDemo.Agents.Weather
Imports AIDemo.Agents.WorkflowInfra
Imports DevExpress.AIIntegration.Chat
Imports OpenAI.Responses
Imports AIDemo.Services
Imports DevExpress.AIIntegration.Agents

#End If
Namespace AIDemo

    Public Partial Class MainWindow

        Private Shared Sub PatchDemoReportsConnectionStrings()
            Dim file As String = DataFilesHelper.FindFile("nwind.db", DataFilesHelper.DataPath)
            Dim directoryName As String = Path.GetDirectoryName(file)
            XtraReportsDemos.ConnectionHelper.SetDataDirectory(directoryName)
        End Sub

        Shared Sub New()
            ' Third-party assemblies (.dll) required to run the demo are embedded inside the executable.
            ' This code loads these DLLs into the current AppDomain
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes()
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf AIDemo.MainWindow.OnAssemblyResolve
            Call RegisterClientAndEmbeddings()
            Call PatchDemoReportsConnectionStrings()
        End Sub

        Private Shared Function OnAssemblyResolve(ByVal sender As Object, ByVal e As ResolveEventArgs) As Assembly
            Dim domain = TryCast(sender, AppDomain)
            Dim assembly = GetType(MainWindow).Assembly
            Dim partialName As String = DevExpress.Utils.AssemblyHelper.GetPartialName(e.Name)
            Dim loadedAssembly = domain.GetAssemblies().SingleOrDefault(Function(a) Equals(a.GetName().Name, partialName))
            If loadedAssembly IsNot Nothing Then Return loadedAssembly
            Dim assemblyName As String = partialName & ".dll"
            Dim filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), assemblyName)
            If File.Exists(filePath) Then
                Return SafeTypeResolver.GetOrLoadAssemblyFile(filePath, False)
            End If

            Dim manifestResourceName As String = assembly.GetManifestResourceNames().FirstOrDefault(Function(x) x.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
            If String.IsNullOrEmpty(manifestResourceName) Then Return Nothing
            Using manifestResourceStream As Stream = assembly.GetManifestResourceStream(manifestResourceName)
                If manifestResourceStream Is Nothing Then Return Nothing
                Dim data As Byte() = New Byte(manifestResourceStream.Length - 1) {}
                manifestResourceStream.Read(data, 0, data.Length)
                Return SafeTypeResolver.GetOrLoadAssemblyBytes(data)
            End Using
        End Function

#If NET
#End If
        Private Shared Async Sub RegisterClientAndEmbeddings()
            Dim azureOpenAIClient = New AzureOpenAIClient(AzureOpenAIEndpoint, AzureOpenAIKey, New AzureOpenAIClientOptions With {.Transport = New PromoteHttpStatusErrorsPipelineTransport()})
            Dim container = AIExtensionsContainerDesktop.Default
            Dim chatClient = azureOpenAIClient.GetChatClient("gpt-4.1").AsIChatClient()
            Dim embeddingGenerator = azureOpenAIClient.GetEmbeddingClient("text-embedding-3-small").AsIEmbeddingGenerator()
            ' 
            ' For demonstration purposes, we're using a simple InMemoryCollection to store vectors in memory,
            ' with a CachingEmbeddingGenerator to load pre-calculated vectors into the vector collection
            ' during the SemanticSearch module initialization.
            ' In real-world applications, it's recommended to use a production-grade vector store.
            ' You can find the list of supported vector store connectors here:
            ' https://learn.microsoft.com/en-us/semantic-kernel/concepts/vector-store-connectors/out-of-the-box-connectors/?pivots=programming-language-csharp
            ' 
            Dim cachingEmbeddingGenerator = New CachingEmbeddingGenerator(embeddingGenerator)
            Dim collection = New InMemoryCollection(Of String, SemanticSearchModule.VectorStoreRecord)(NameOf(SemanticSearchDataHelper.Movies), New InMemoryCollectionOptions With {.EmbeddingGenerator = cachingEmbeddingGenerator})
            Dim serviceCollection = New ServiceCollection()
#If NET
            ' To avoid unintended coupling between DevExpress AI-powered Extensions and the tool-calling pipeline,
            ' use separate IChatClient instances (one for AI-powered Extensions and one for the AI Chat Control with AITools).
            serviceCollection.AddKeyedScoped(AIToolsModule.ToolsChatResponseProviderServiceKey, Function(provider, key) New ChatClientBuilder(chatClient).UseDXTools().UseFunctionInvocation().Build(provider).AsIChatResponseProvider())
            ' Agentic AI: build a function-invocation-enabled IChatClient for agent workflows
            Dim agentChatClient = chatClient.AsBuilder().UseFunctionInvocation().Build()
            Dim agentFactory = New AgentFactory(agentChatClient)
            Dim toolCallingAgent = agentChatClient.AsAIAgent(name:="ToolCallingAgent", instructions:="You are a helpful assistant with access to utility tools. Use the available tools to answer user questions.", tools:=SimpleTools.Tools)
            Dim toolCallingSession = Await toolCallingAgent.CreateSessionAsync()
            ' Simple AIAgent with tool calling
            serviceCollection.AddKeyedTransient(ToolCallingAgentModule.ServiceKey, Function(sp, __) toolCallingAgent.AsIChatResponseProvider(toolCallingSession))
            ' Shakespearean Poet workflow (non-streaming, buffered)
            serviceCollection.AddKeyedScoped(WorkflowAsAgentModule.ServiceKey, Function(sp, key) agentFactory.CreateShakespeareanPoetAgenticWorkflow())
            ' Shakespearean Poet workflow (streaming with workflow events) for Agent module demo
            serviceCollection.AddKeyedScoped(Of IChatResponseProvider)(WorkflowResponseStreamingModule.ServiceKey, Function(sp, key) New WorkflowResponseProvider(Of String)(agentFactory.CreateShakespeareanPoetWorkflow(), AddressOf AIDemo.MainWindow.GetLastUserText))
            ''' Text Processing pipeline: Uppercase → Reverse (streaming)
            serviceCollection.AddKeyedScoped(Of IChatResponseProvider)(WorkflowWithExecutorsModule.ServiceKey, Function(sp, key) New WorkflowResponseProvider(Of String)(AgentFactory.CreateTextProcessingWorkflow(), AddressOf AIDemo.MainWindow.GetLastUserText))
            ' Group Chat Workflow: Content Reviewer + Publisher with tool calling approval
            serviceCollection.AddKeyedScoped(Of IChatResponseProvider)(GroupChatWorkflowModule.ServiceKey, Function(sp, key) New WorkflowResponseProvider(Of List(Of ChatMessage))(agentFactory.CreatePublishingGroupChatWorkflow(), Function(messages) messages.ToList(), WorkflowResponseProviderStartMode.Open))
            ' Reasoning: Azure OpenAI Responses API with reasoning options
            serviceCollection.AddKeyedScoped(ReasoningModule.ServiceKey, Function(sp, __)
                Dim baseOptions = New CreateResponseOptions With {.ReasoningOptions = New ResponseReasoningOptions() With {.ReasoningEffortLevel = ResponseReasoningEffortLevel.Medium, .ReasoningSummaryVerbosity = ResponseReasoningSummaryVerbosity.Detailed}}
                Dim responsesClient = azureOpenAIClient.GetResponsesClient()
                Dim reasoningClient = responsesClient.AsIChatClient().AsBuilder().ConfigureOptions(Sub(x)
                    x.RawRepresentationFactory = Function(_raw) baseOptions
                End Sub).Build()
                Return reasoningClient.AsIChatResponseProvider()
            End Function)
            ' AG-UI: self-host a Kestrel server with the Weather Agent endpoint
            Dim weatherService = New WeatherService()
            Dim weatherTools = New WeatherAgentTools(weatherService)
            Dim aguiHost = Await Services.AGUIHost.StartAsync(agentFactory, weatherTools)
            AddHandler Application.Current.Exit, Sub(sender, e) aguiHost.Dispose()
            serviceCollection.AddKeyedScoped(AGUIIntegrationModule.ServiceKey, Function(sp, key) AgentFactory.CreateAGUIWeatherResponseProvider(aguiHost.GetWeatherAgentEndpoint()))
#End If
            serviceCollection.AddChatClient(chatClient)
            serviceCollection.AddDevExpressAIDesktop(Sub(x)
                x.RegisterVectorCollection(collection)
                x.RegisterAIExceptionHandler(New AIExceptionHandler())
            End Sub)
        End Sub

        Private Shared Function GetLastUserText(ByVal messages As IEnumerable(Of ChatMessage)) As String
            Dim userMessage = messages.LastOrDefault(Function(m) m.Role = ChatRole.User)
            If userMessage Is Nothing OrElse String.IsNullOrWhiteSpace(userMessage.Text) Then Throw New InvalidOperationException("The workflow requires a user text message.")
            Return userMessage.Text
        End Function

        Private Shared ReadOnly Property AzureOpenAIEndpoint As Uri
            Get
                Dim lAzureOpenAIEndpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
                If String.IsNullOrEmpty(lAzureOpenAIEndpoint) Then lAzureOpenAIEndpoint = "https://public-api.devexpress.com/demo-openai" 'DevExpress proxy-server
                Return New Uri(lAzureOpenAIEndpoint)
            End Get
        End Property

        Private Shared ReadOnly Property AzureOpenAIKey As ClientModel.ApiKeyCredential
            Get
                Dim lAzureOpenAIKey = GetEnvironmentVariable("AZURE_OPENAI_API_KEY")
                If String.IsNullOrEmpty(lAzureOpenAIKey) Then lAzureOpenAIKey = "DEMO" 'Demo key
                Return New ClientModel.ApiKeyCredential(lAzureOpenAIKey)
            End Get
        End Property

        Private Shared Function GetEnvironmentVariable(ByVal variableName As String) As String
            Return SafeEnvironment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User)
        End Function

        Public Sub New()
            InitializeComponent()
        End Sub

        'WA for https://github.com/Azure/azure-sdk-for-net/issues/45618
        Private Class PromoteHttpStatusErrorsPipelineTransport
            Inherits HttpClientPipelineTransport

            Protected Overrides Function CreateMessageCore() As PipelineMessage
                Dim message = MyBase.CreateMessageCore()
                Return New DemoPipelineMessage(message.Request)
            End Function

            Protected Overrides Sub OnReceivedResponse(ByVal message As PipelineMessage, ByVal httpResponse As HttpResponseMessage)
                If Not httpResponse.IsSuccessStatusCode Then
                    If CInt(httpResponse.StatusCode) = 429 Then
                        Dim secondsString As String = httpResponse.Headers.GetValues("Retry-After").FirstOrDefault()
                        Dim seconds As Integer = Integer.Parse(secondsString)
                        Dim text As String = $"Our demo AI Service reached a temporary request limit. Your operation will resume automatically in {secondsString} seconds. You can press Cancel to abort the operation."
                        Dim activeWindow = ActiveWindowHelper.GetActiveWindow()
                        activeWindow.Dispatcher.Invoke(Sub()
                            Dim result = ThemedMessageBox.Show("AI Service Request Limit", text, MessageBoxButton.OKCancel, MessageBoxResult.OK, New ThemedMessageBoxParameters(MessageBoxImage.Warning) With {.Owner = activeWindow, .TimerTimeout = TimeSpan.FromSeconds(seconds)})
                            If result = MessageBoxResult.Cancel Then TryCast(message, DemoPipelineMessage)?.Cancel()
                        End Sub)
                    Else
                        Throw New HttpRequestException("HTTP request failed with status code: " & httpResponse.StatusCode)
                    End If
                End If

                MyBase.OnReceivedResponse(message, httpResponse)
            End Sub

            Private Class DemoPipelineMessage
                Inherits PipelineMessage

                Private ReadOnly cts As CancellationTokenSource = New CancellationTokenSource()

                Public Sub New(ByVal request As PipelineRequest)
                    MyBase.New(request)
                    CancellationToken = cts.Token
                End Sub

                Public Sub Cancel()
                    cts.Cancel()
                End Sub
            End Class
        End Class
    End Class
End Namespace
