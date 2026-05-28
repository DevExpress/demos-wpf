using System;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Data.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoLauncher;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Internal;
using DevExpress.Xpf.DemoCenterBase;
using DevExpress.Xpf.DemoBase;

namespace ProductsDemo {
    public partial class App : Application {
        static App() {
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
        }
        public App() {
            ExceptionHelper.Initialize();
            DevExpress.Images.ImagesAssemblyLoader.Load();
            DevExpress.Xpf.DemoBase.AssemblyResolver.Subscribe();
            ApplicationThemeHelper.ApplicationThemeName = DemoStartupOptions.ActualDefaultTheme.Name;
            DemoRunner.ShowApplicationSplashScreen();
            Theme.RegisterPredefinedPaletteThemes();
        }
        protected override void OnStartup(StartupEventArgs e) {
#if !NET
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
            base.OnStartup(e);
        }
    }
}
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new ProductsDemo.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new ProductsDemo.MainWindow();
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
