using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using DevExpress.Data.Internal;
using DevExpress.Data.Utils;
using DevExpress.Internal;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Core;

namespace DevExpress.VideoRent.Wpf {
    class App : Application {
        public App() {
            Startup += OnStartup;
        }
        public event EventHandler BeforeMainWindowClosed;
        void OnStartup(object sender, StartupEventArgs e) {
            MainWindow mainWindow = new MainWindow();
            mainWindow.BeforeClosed += OnMainWindowBeforeClosed;
            mainWindow.Show();
        }
        void OnMainWindowBeforeClosed(object sender, EventArgs e) {
            if(BeforeMainWindowClosed != null)
                BeforeMainWindowClosed(this, EventArgs.Empty);
        }
    }
    static class Program {
        const string iniFilePath = "VideoRent.ini";
        [STAThread]
        static void Main(string[] args) {
            ExceptionHelper.Initialize();
            new System.Windows.Documents.FlowDocument();
            foreach(var theme in Theme.Themes.Where(t => t.Name.Contains("Touch")))
                theme.ShowInThemeSelector = false;
            ApplicationThemeHelper.ApplicationThemeName = WpfLayoutData.DefaultApplicationTheme.Name;
            WpfViewsManager.RegisterHelperViews();
            IniFile iniFile = new IniFile();
            if(File.Exists(iniFilePath)) iniFile.Load(iniFilePath);
            InitialDbCreator initialDbCreator = new InitialDbCreator(new CreateInitialDbDialog(), ExceptionProcesser.Current);
            if(!initialDbCreator.OpenDb(iniFile)) return;
            iniFile.Save(iniFilePath);
            //TODO Create Login-Dialog
            if(!LayoutManager.Current.Login(ReferenceData.AdministratorString, string.Empty)) return;
            App app = new App();
            app.BeforeMainWindowClosed += (d, e) => { LayoutManager.Current.Logout(); };
            app.Run();
        }
        #region DemoTester
        public static void Test(ThemedWindow window) {
            var frameworkPrefix = "Framework4";
#pragma warning disable DX0025 // no user input
            var testerPath = string.Format(@"c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort);
#pragma warning restore DX0025
            var args = SafeEnvironment.GetCommandLineArgs();
            if(File.Exists(testerPath) && args.Any() && args.Contains(@"/testallexcept:")) {
                ExceptionHelper.IsEnabled = false;
                var assembly = SafeTypeResolver.GetOrLoadAssemblyFrom(testerPath);
                var type = SafeTypeResolver.GetKnownType(assembly, "DevExpress.Reallife.Tester.VideoRentDemoTester");
                type.GetMethod("Create")?.Invoke(null, new object[] { window, true });
            }
        }
        #endregion
    }
}
