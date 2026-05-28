using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadData(true, true);
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid,
                PreloadCategories.Charts
            );
        }

        public static MainWindow Instance { get; private set; }
        public static ChartsDemoRibbon CommonRibbon { get { return (ChartsDemoRibbon)Instance.Ribbon; } }
        
        public MainWindow() {
            Instance = this;
            DataContext = ViewModelSource.Create(() => new MainViewModel());
            InitializeComponent();
        }
        protected override void OnHideDemoModule(DemoModule demo) {
            base.OnHideDemoModule(demo);
            (demo as ChartsDemoModule)?.OnStopModule();
            CommonRibbon.Clear();
        }
        protected override void OnDemoModuleCompletelyLoaded(DemoModule demo) {
            base.OnDemoModuleCompletelyLoaded(demo);
            (demo as ChartsDemoModule)?.OnStartModule();
        }
    }
    public class MainViewModel : ViewModelBase { }
}
