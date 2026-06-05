using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevExpress.Data.Internal;
using DevExpress.Data.Utils;
using DevExpress.Images;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoCenterBase;
#if NET
using Microsoft.Extensions.AI;
using DevExpress.AIIntegration;
using Azure.AI.OpenAI;
using DevExpress.DevAV.Common.Utils;
using System.Net.Http;
using System.ClientModel.Primitives;
using DevExpress.AIIntegration.Wpf.Chat;
#endif

namespace DevExpress.DevAV {
    public partial class App : Application {
        static IDisposable singleInstanceApplicationGuard;
        static App() {
            CompatibilitySettings.UseLightweightThemes = true;
            ApplicationThemeHelper.ApplicationThemeName = "Win11Light";
            bool exit;
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", out exit);
            if(exit) {
                SafeEnvironment.Exit(0);
            }

            DemoRunner.ShowApplicationSplashScreen();
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
        }

#if NET
        
        
        
        public static AIExtensionsContainerDefault ToolChatContainerDefault { get; set; } = new AIExtensionsContainerDefault();
#endif
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            DevAVDataDirectoryHelper.LocalPrefix = "WpfOutlookInspiredApp";
#if !NET
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
            AssemblyResolver.Subscribe();
            Theme.RegisterPredefinedPaletteThemes();
            ImagesAssemblyLoader.Load();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            base.OnStartup(e);
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            Theme.TouchlineDark.ShowInThemeSelector = false;
            SetCultureInfo();
#if NET
            RegisterChatClient();
#endif
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            demoCI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.NumberFormat.CurrencySymbol = "$";
            demoUI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }

#if NET
        static void RegisterChatClient() {
            AzureOpenAIClient azureOpenAIClient = new AzureOpenAIClient(AzureOpenAIEndpoint, AzureOpenAIKey, new AzureOpenAIClientOptions() {
                Transport = new PromoteHttpStatusErrorsPipelineTransport()
            });
            IChatClient chatClient = azureOpenAIClient.GetChatClient("demo").AsIChatClient();
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

#endif


        #region DemoTester
        public static void Test(ThemedWindow window) {
            string testerPath = null;
            bool allowDemoTest = false;
            var frameworkPrefix =
#if NET
                "NETCoreDesktop";
#else
                "Framework4";
#endif
#pragma warning disable DX0025 
            testerPath = string.Format(@"c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort);
#pragma warning restore DX0025
            var args = SafeEnvironment.GetCommandLineArgs();
            allowDemoTest = args.Length > 0 && args.Contains(@"/testallexcept:");

            if(File.Exists(testerPath) && allowDemoTest) {
                ExceptionHelper.IsEnabled = false;
                var assembly = SafeTypeResolver.GetOrLoadAssemblyFrom(testerPath);
                var type = SafeTypeResolver.GetKnownType(assembly, "DevExpress.Reallife.Tester.OutlookInspiredAppDemoTester");
                type.GetMethod("Create")?.Invoke(null, new object[] { window, true });
            }
        }

        #endregion

        public static string ApplicationID {
            get { return string.Format("Components_{0}_Demo_Center_OutlookInspired_{0}", AssemblyInfo.VersionShort.Replace(".", "_")); }
        }
    }

    [Guid("86882B9F-1EAE-41D9-B9CF-BD7ACCA51523"), ComVisible(true)]
    public class OutlookInspiredAppNotificationActivator : ToastNotificationActivator {
        public override void OnActivate(string arguments, System.Collections.Generic.Dictionary<string, string> data) { }
    }
}

#if NET

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
#endif

namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.DevAV.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new DevExpress.DevAV.MainWindow();
                    window.Show();
                    app.MainWindow = window;
                    done.SetResult(window);
                };
                app.Run();
            };
            return Tuple.Create(run, done.Task);
        }
    }
}
