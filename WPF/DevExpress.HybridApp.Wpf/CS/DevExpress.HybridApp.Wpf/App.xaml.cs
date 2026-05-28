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
using DevExpress.Images;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoCenterBase;
using DevExpress.Xpf.Editors;

namespace DevExpress.DevAV {
    public partial class App : Application {
        static IDisposable singleInstanceApplicationGuard;

        static App() {
            CompatibilitySettings.UseLegacyColumnFilterPopup = true;
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
        }
        public App() {
            ApplicationThemeHelper.ApplicationThemeName = Theme.HybridApp.Name;
        }
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            ClearAutomationEventsHelper.IsEnabled = false;
            DevAVDataDirectoryHelper.LocalPrefix = "WpfHybridApp";
#if !NET
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
            ImagesAssemblyLoader.Load();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));

            base.OnStartup(e);
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            bool exit;
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfHybridApp", out exit);
            if(exit) {
                Shutdown();
                return;
            }
            SetCultureInfo();
            SetLocalization();
            DemoRunner.ShowApplicationSplashScreen();
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
        static void SetLocalization() {
            EditorLocalizer.Active = new HybridAppEditorLocalizer();
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
                var type = SafeTypeResolver.GetKnownType(assembly, "DevExpress.Reallife.Tester.HybridAppDemoTester");
                type.GetMethod("Create")?.Invoke(null, new object[] { window, true });
            }
        }
        #endregion
    }
    public class HybridAppEditorLocalizer : EditorLocalizer {
        protected override void PopulateStringTable() {
            base.PopulateStringTable();
            AddString(EditorStringId.LookUpSearch, "Enter text to search...");
        }
    }
}

namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                DemoRunner.BypassSealedCheck = true;
                var app = new DevExpress.DevAV.App();
                DemoRunner.BypassSealedCheck = false;
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
