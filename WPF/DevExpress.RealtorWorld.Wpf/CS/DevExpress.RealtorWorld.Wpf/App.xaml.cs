using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevExpress.Data.Internal;
using DevExpress.Data.Utils;
using DevExpress.Internal;
using DevExpress.Xpf.Core;

namespace DevExpress.RealtorWorld.Xpf {
    public partial class App : Application {
        static App() {
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
        }
        public App() {
            ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDark.Name;
        }
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
#if !NET
            DataDirectoryHelper.SetWebBrowserMode();
#endif
            base.OnStartup(e);
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            SetCultureInfo();
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
                var type = SafeTypeResolver.GetKnownType(assembly, "DevExpress.Reallife.Tester.RealtorWorldDemoTester");
                type.GetMethod("Create")?.Invoke(null, new object[] { window, true });
            }
        }
        #endregion
    }
}
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.RealtorWorld.Xpf.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new DevExpress.RealtorWorld.Xpf.View.MainWindow();
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
