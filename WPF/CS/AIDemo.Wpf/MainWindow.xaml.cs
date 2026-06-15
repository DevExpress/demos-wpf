using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Windows;
using AIDemo.SemanticSearch;
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using DevExpress.Data.Internal;
using DevExpress.Data.Utils;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.InMemory;

#if NET
using AIDemo.Agents;
using AIDemo.Agents.Weather;
using AIDemo.Agents.WorkflowInfra;
using DevExpress.AIIntegration.Chat;
using OpenAI.Responses;
using AIDemo.Services;
using DevExpress.AIIntegration.Agents;
#endif

namespace AIDemo {
    public partial class MainWindow {
        static void PatchDemoReportsConnectionStrings() {
            string file = DataFilesHelper.FindFile("nwind.db", DataFilesHelper.DataPath);
            string directoryName = System.IO.Path.GetDirectoryName(file);
            XtraReportsDemos.ConnectionHelper.SetDataDirectory(directoryName);
        }

        static MainWindow() {
            
            
            XtraReportsDemos.ObjectDataSourceTypesRegistrator.RegisterTrustedTypes();
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
            RegisterClientAndEmbeddings();
            PatchDemoReportsConnectionStrings();
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs e) {
            var domain = sender as AppDomain;
            var assembly = typeof(MainWindow).Assembly;
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(e.Name);
            var loadedAssembly = domain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == partialName);
            if(loadedAssembly != null)
                return loadedAssembly;
            string assemblyName = partialName + ".dll";
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), assemblyName);
            if(File.Exists(filePath)) {
                return SafeTypeResolver.GetOrLoadAssemblyFile(filePath, false);
            }

            string manifestResourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(x => x.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase));
            if(string.IsNullOrEmpty(manifestResourceName))
                return null;
            using(Stream manifestResourceStream = assembly.GetManifestResourceStream(manifestResourceName)) {
                if(manifestResourceStream == null)
                    return null;
                byte[] data = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(data, 0, data.Length);
                return SafeTypeResolver.GetOrLoadAssemblyBytes(data);
            }
        }

        static
#if NET
            async
#endif
            void RegisterClientAndEmbeddings() {
            var azureOpenAIClient = new AzureOpenAIClient(AzureOpenAIEndpoint, AzureOpenAIKey, new AzureOpenAIClientOptions {
                Transport = new PromoteHttpStatusErrorsPipelineTransport()
            });

            var container = AIExtensionsContainerDesktop.Default;
            var chatClient = azureOpenAIClient.GetChatClient("demo").AsIChatClient();
            var embeddingGenerator = azureOpenAIClient.GetEmbeddingClient("text-embedding-3-small").AsIEmbeddingGenerator();
            
            var cachingEmbeddingGenerator = new CachingEmbeddingGenerator(embeddingGenerator);
            var collection = new InMemoryCollection<string, SemanticSearchModule.VectorStoreRecord>(nameof(SemanticSearchDataHelper.Movies),
                new InMemoryCollectionOptions { EmbeddingGenerator = cachingEmbeddingGenerator });

            var serviceCollection = new ServiceCollection();
#if NET
            
            
            serviceCollection.AddKeyedScoped<IChatResponseProvider>(AIToolsModule.ToolsChatResponseProviderServiceKey, (provider, key) => {
                return new ChatClientBuilder(chatClient)
                   .UseDXTools()
                   .UseFunctionInvocation()
                   .Build(provider).AsIChatResponseProvider();
            });

            
            var agentChatClient = chatClient
                        .AsBuilder()
                        .UseFunctionInvocation()
                        .Build();
            var agentFactory = new AgentFactory(agentChatClient);

            var toolCallingAgent = agentChatClient.AsAIAgent(
                name: "ToolCallingAgent",
                instructions: "You are a helpful assistant with access to utility tools. Use the available tools to answer user questions.",
                tools: SimpleTools.Tools);
            var toolCallingSession = await toolCallingAgent.CreateSessionAsync();

            
            serviceCollection.AddKeyedTransient<IChatResponseProvider>(ToolCallingAgentModule.ServiceKey, (sp, _) => {
                return toolCallingAgent.AsIChatResponseProvider(toolCallingSession);
            });

            
            serviceCollection.AddKeyedScoped<IChatResponseProvider>(WorkflowAsAgentModule.ServiceKey, (sp, key) => {
                return agentFactory.CreateShakespeareanPoetAgenticWorkflow();
            });

            
            serviceCollection.AddKeyedScoped<IChatResponseProvider>(WorkflowResponseStreamingModule.ServiceKey, (sp, key) => {
                return new WorkflowResponseProvider<string>(agentFactory.CreateShakespeareanPoetWorkflow(), GetLastUserText);
            });

            
            serviceCollection.AddKeyedScoped<IChatResponseProvider>(WorkflowWithExecutorsModule.ServiceKey, (sp, key) => {
                return new WorkflowResponseProvider<string>(AgentFactory.CreateTextProcessingWorkflow(), GetLastUserText);
            });

            
            var toolApprovalAgent = agentFactory.CreateToolApprovalAgent();
            var toolApprovalSession = await toolApprovalAgent.CreateSessionAsync();

            serviceCollection.AddKeyedTransient<IChatResponseProvider>(AIAgentWithToolApprovalModule.ServiceKey, (sp, _) => {
                return toolApprovalAgent.AsIChatResponseProvider(toolApprovalSession);
            });

            
            serviceCollection.AddKeyedScoped<IChatResponseProvider>(ReasoningModule.ServiceKey, (sp, _) => {
                var baseOptions = new CreateResponseOptions {
                    ReasoningOptions = new ResponseReasoningOptions() {
                        ReasoningEffortLevel = ResponseReasoningEffortLevel.Medium,
                        ReasoningSummaryVerbosity = ResponseReasoningSummaryVerbosity.Detailed
                    }
                };
                var responsesClient = azureOpenAIClient.GetResponsesClient();
                var reasoningClient = responsesClient.AsIChatClient("demo-mini").AsBuilder().ConfigureOptions(x => {
                    x.RawRepresentationFactory = _ => baseOptions;
                }).Build();
                return reasoningClient.AsIChatResponseProvider();
            });

            
            var weatherService = new WeatherService();
            var weatherTools = new WeatherAgentTools(weatherService);
            var aguiHost = await AGUIHost.StartAsync(agentFactory, weatherTools);

            Application.Current.Exit += (sender, e) => aguiHost.Dispose();

            serviceCollection.AddKeyedScoped<IChatResponseProvider>(AGUIIntegrationModule.ServiceKey, (sp, key) => {
                return AgentFactory.CreateAGUIWeatherResponseProvider(aguiHost.GetWeatherAgentEndpoint());
            });
#endif
                serviceCollection.AddChatClient(chatClient);
            serviceCollection.AddDevExpressAIDesktop(x => {
                x.RegisterVectorCollection(collection);
                x.RegisterAIExceptionHandler(new AIExceptionHandler());
            });
        }

        static string GetLastUserText(IEnumerable<ChatMessage> messages) {
            var userMessage = messages.LastOrDefault(m => m.Role == ChatRole.User);
            if(userMessage is null || string.IsNullOrWhiteSpace(userMessage.Text))
                throw new InvalidOperationException("The workflow requires a user text message.");
            return userMessage.Text;
        }

        static Uri AzureOpenAIEndpoint {
            get {
                var azureOpenAIEndpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
                if(string.IsNullOrEmpty(azureOpenAIEndpoint))
                    azureOpenAIEndpoint = "https://api.devexpress.com/demo-openai"; 
                return new Uri(azureOpenAIEndpoint);
            }
        }

        static System.ClientModel.ApiKeyCredential AzureOpenAIKey {
            get {
                var azureOpenAIKey = GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
                if(string.IsNullOrEmpty(azureOpenAIKey))
                    azureOpenAIKey = "DEMO"; 
                return new System.ClientModel.ApiKeyCredential(azureOpenAIKey);
            }
        }

        static string GetEnvironmentVariable(string variableName) {
            return SafeEnvironment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);
        }

        public MainWindow() {
            InitializeComponent();
        }

        
        class PromoteHttpStatusErrorsPipelineTransport : HttpClientPipelineTransport {

            protected override PipelineMessage CreateMessageCore() {
                var message = base.CreateMessageCore();
                return new DemoPipelineMessage(message.Request);
            }

            protected override void OnReceivedResponse(PipelineMessage message, HttpResponseMessage httpResponse) {
                if(!httpResponse.IsSuccessStatusCode) {
                    if((int)httpResponse.StatusCode == 429) {
                        string secondsString = httpResponse.Headers.GetValues("Retry-After").FirstOrDefault();
                        int seconds = int.Parse(secondsString);
                        string text = $"Our demo AI Service reached a temporary request limit. Your operation will resume automatically in {secondsString} seconds. You can press Cancel to abort the operation.";
                        var activeWindow = ActiveWindowHelper.GetActiveWindow();
                        activeWindow.Dispatcher.Invoke(() => {
                            var result = ThemedMessageBox.Show("AI Service Request Limit", text, MessageBoxButton.OKCancel, MessageBoxResult.OK, new ThemedMessageBoxParameters(MessageBoxImage.Warning) { Owner = activeWindow, TimerTimeout = TimeSpan.FromSeconds(seconds) });
                            if(result == MessageBoxResult.Cancel)
                                (message as DemoPipelineMessage)?.Cancel();
                        });
                    }
                    else {
                        throw new HttpRequestException("HTTP request failed with status code: " + httpResponse.StatusCode);
                    }
                }

                base.OnReceivedResponse(message, httpResponse);
            }

            class DemoPipelineMessage : PipelineMessage {
                readonly CancellationTokenSource cts = new CancellationTokenSource();

                public DemoPipelineMessage(PipelineRequest request) : base(request) {
                    CancellationToken = cts.Token;
                }

                public void Cancel() => cts.Cancel();
            }
        }
    }
}

