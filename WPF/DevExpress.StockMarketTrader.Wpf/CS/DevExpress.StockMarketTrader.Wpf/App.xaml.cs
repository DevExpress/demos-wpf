using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoCenterBase;
using System;
using System.Windows;

namespace DevExpress.StockMarketTrader {
    public partial class App : Application {
        static App() {
            DevExpress.Xpf.Core.ClearAutomationEventsHelper.IsEnabled = false;
        }
        protected override void OnStartup(StartupEventArgs e) {
            ApplicationThemeHelper.ApplicationThemeName = DemoStartupOptions.ActualDefaultTheme.Name;
            DemoRunner.ShowApplicationSplashScreen();
            base.OnStartup(e);
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
        }
    }
}
