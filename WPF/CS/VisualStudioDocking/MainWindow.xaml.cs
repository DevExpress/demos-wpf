using System;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace VisualStudioDocking {
    public partial class MainWindow : ThemedWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadData(true, false);
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.Accordion,
                PreloadCategories.Docking,
                PreloadCategories.PropertyGrid
            );
        }
        static void ShowSplashScreen() {
            var viewModel = new DXSplashScreenViewModel() {
                Title = "Visual Studio Inspired UI Demo",
                Subtitle = "Powered by DevExpress",
                Logo = new Uri(string.Format(@"pack://application:,,,/DevExpress.Xpf.DemoBase.v{0};component/DemoLauncher/Images/Logo.svg", AssemblyInfo.VersionShort))
            };
            SplashScreenManager.Create(() => new DockingSplashScreenWindow(), viewModel).ShowOnStartup();
        }

        public MainWindow() {
            ApplicationThemeHelper.ApplicationThemeName = Theme.VS2019Light.Name;
            ShowSplashScreen();
            InitializeComponent();
        }
    }
    public class CodeViewPresenter : DevExpress.Xpf.DemoBase.Helpers.Internal.CodeViewPresenter { }
}
