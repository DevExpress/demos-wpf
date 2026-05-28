using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using DevExpress.Data.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;
using DevExpress.WindowsMailClient.Wpf.Internal;
using DevExpress.Xpf.DemoCenterBase;
using DevExpress.Xpf.DemoBase;

namespace DevExpress.WindowsMailClient.Wpf {
    public partial class App : Application {
        Bootstrapper bootstrapper;
        static App() {
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
        }
        protected override void OnStartup(StartupEventArgs e) {
            SetCultureInfo();
            StrategyManager.Default.RegisterStrategy<HamburgerMenu, HamburgerMenuStrategy>();
#if !NET
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
            ApplicationThemeHelper.ApplicationThemeName = DemoStartupOptions.ActualDefaultTheme.Name;
            DemoRunner.ShowApplicationSplashScreen();
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            base.OnStartup(e);
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }
    }
}
