using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace SchedulingDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadData(true, false);
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid,
                PreloadCategories.Scheduling,
                PreloadCategories.Printing
            );
        }

        public static MainWindow Instance { get; private set; }
        public static SchedulerDemoRibbon CommonRibbon { get { return (SchedulerDemoRibbon)Instance.Ribbon; } }

        public MainWindow() {
            Instance = this;
            InitializeComponent();
        }
    }
}
