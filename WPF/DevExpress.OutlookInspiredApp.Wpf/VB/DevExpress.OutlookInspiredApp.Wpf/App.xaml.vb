Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Windows
Imports System.Windows.Media.Animation
Imports DevExpress.Data.Internal
Imports DevExpress.Data.Utils
Imports DevExpress.Images
Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.DemoCenterBase

#If NET
using Microsoft.Extensions.AI;
using DevExpress.AIIntegration;
using Azure.AI.OpenAI;
using DevExpress.DevAV.Common.Utils;
using System.Net.Http;
using System.ClientModel.Primitives;
using DevExpress.AIIntegration.Wpf.Chat;
#End If
Namespace DevExpress.DevAV

    Public Partial Class App
        Inherits Application

        Private Shared singleInstanceApplicationGuard As IDisposable

        Shared Sub New()
            CompatibilitySettings.UseLightweightThemes = True
            ApplicationThemeHelper.ApplicationThemeName = "Win11Light"
            Dim [exit] As Boolean
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", [exit])
            If [exit] Then
                SafeEnvironment.Exit(0)
            End If

            Call DemoRunner.ShowApplicationSplashScreen()
            ClearAutomationEventsHelper.IsEnabled = False
        End Sub

#If NET
        
        
        
        public static AIExtensionsContainerDefault ToolChatContainerDefault { get; set; } = new AIExtensionsContainerDefault();
#End If
        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Call ExceptionHelper.Initialize()
            DevAVDataDirectoryHelper.LocalPrefix = "WpfOutlookInspiredApp"
#If Not NET
            Call ServiceContainer.Default.RegisterService(New ApplicationJumpListService())
#End If
            Call AssemblyResolver.Subscribe()
            Call Theme.RegisterPredefinedPaletteThemes()
            Call ImagesAssemblyLoader.Load()
            Call Timeline.DesiredFrameRateProperty.OverrideMetadata(GetType(Timeline), New FrameworkPropertyMetadata(200))
            MyBase.OnStartup(e)
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            Theme.TouchlineDark.ShowInThemeSelector = False
            Call SetCultureInfo()
#If NET
            RegisterChatClient();
#End If
        End Sub

        Private Shared Sub SetCultureInfo()
            Dim demoCI As CultureInfo = CType(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
            demoCI.NumberFormat.CurrencySymbol = "$"
            demoCI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentCulture = demoCI
            Dim demoUI As CultureInfo = CType(Thread.CurrentThread.CurrentUICulture.Clone(), CultureInfo)
            demoUI.NumberFormat.CurrencySymbol = "$"
            demoUI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentUICulture = demoUI
        End Sub

#If NET
        static void RegisterChatClient() {
            AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(AzureOpenAIEndpoint, AzureOpenAIKey, new AzureOpenAIClientOptions() {
                Transport = new PromoteHttpStatusErrorsPipelineTransport()
            });
            IChatClient chatClient = azureOpenAIClient.GetChatClient("gpt-4.1").AsIChatClient();
            var container = AIExtensionsContainerDesktop.Default;

            IChatClient toolChatClient = new ChatClientBuilder(chatClient)
                .UseDXTools()
                .UseFunctionInvocation(configure: x => x.IncludeDetailedErrors = true)
                .Build(ToolChatContainerDefault);

            ToolChatContainerDefault.RegisterChatClient(toolChatClient);
            ToolChatContainerDefault.RegisterAIExceptionHandler(new AIExceptionHandler());

            AIExtensionsContainerDesktop.QueryIAIExtensionsContainer += (s, e) => {
                if(s is AIChatControl)
                    e.Container = ToolChatContainerDefault;
            };

            container.RegisterChatClient(chatClient);
            container.RegisterAIExceptionHandler(new AIExceptionHandler());
        }

        static Uri AzureOpenAIEndpoint {
            get {
                var azureOpenAIEndpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
                if(string.IsNullOrEmpty(azureOpenAIEndpoint))
                    azureOpenAIEndpoint = "https://public-api.devexpress.com/demo-openai"; 
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

#End If
#Region "DemoTester"
        Public Shared Sub Test(ByVal window As ThemedWindow)
            Dim testerPath As String = Nothing
            Dim allowDemoTest As Boolean = False
#If NET
                "NETCoreDesktop";
#Else
            Dim frameworkPrefix = "Framework4"
#End If
            testerPath = String.Format("c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort)
            Dim args = SafeEnvironment.GetCommandLineArgs()
            allowDemoTest = args.Length > 0 AndAlso args.Contains("/testallexcept:")
            If File.Exists(testerPath) AndAlso allowDemoTest Then
                ExceptionHelper.IsEnabled = False
                Dim assembly = SafeTypeResolver.GetOrLoadAssemblyFrom(testerPath)
                Dim type = SafeTypeResolver.GetKnownType(assembly, "DevExpress.Reallife.Tester.OutlookInspiredAppDemoTester")
                type.GetMethod("Create")?.Invoke(Nothing, New Object() {window, True})
            End If
        End Sub

#End Region
        Public Shared ReadOnly Property ApplicationID As String
            Get
                Return String.Format("Components_{0}_Demo_Center_OutlookInspired_{0}", AssemblyInfo.VersionShort.Replace(".", "_"))
            End Get
        End Property
    End Class

    <Guid("86882B9F-1EAE-41D9-B9CF-BD7ACCA51523"), ComVisible(True)>
    Public Class OutlookInspiredAppNotificationActivator
        Inherits ToastNotificationActivator

        Public Overrides Sub OnActivate(ByVal arguments As String, ByVal data As Collections.Generic.Dictionary(Of String, String))
        End Sub
    End Class
End Namespace

#If NET

class PromoteHttpStatusErrorsPipelineTransport : HttpClientPipelineTransport {
    protected override void OnReceivedResponse(PipelineMessage message, HttpResponseMessage httpResponse) {
        if(!httpResponse.IsSuccessStatusCode) {
            if((int)httpResponse.StatusCode == 429)
                throw new AIDemoException("You have reached demo request limit. Further requests are temporarily suspended. Please try again in a few minutes. Thank you for your patience and understanding.");
            throw new HttpRequestException("HTTP request failed with status code: " + httpResponse.StatusCode);
        }

        base.OnReceivedResponse(message, httpResponse);
    }
}
#End If
Namespace DevExpress.Internal.DemoLauncher

    Public Module DemoLauncherEntryPoint

        Public Function Run() As Tuple(Of Action, Tasks.Task(Of Window))
            Dim done = New Tasks.TaskCompletionSource(Of Window)()
            Dim lRun As Action = Sub()
                Dim app = New DevAV.App()
                app.InitializeComponent()
                Call GetType(Application).GetField("_startupUri", BindingFlags.Instance Or BindingFlags.NonPublic).SetValue(app, Nothing)
                AddHandler app.Startup, Sub(s, e)
                    Dim window = New DevAV.MainWindow()
                    window.Show()
                    app.MainWindow = window
                    done.SetResult(window)
                End Sub
                app.Run()
            End Sub
            Return Tuple.Create(lRun, done.Task)
        End Function
    End Module
End Namespace
