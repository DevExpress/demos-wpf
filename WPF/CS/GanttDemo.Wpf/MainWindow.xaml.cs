using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace GanttDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid,
                PreloadCategories.Printing
            );
        }

        public static MainWindow Instance { get; private set; }
        public static GanttDemoRibbon CommonRibbon { get { return (GanttDemoRibbon)Instance.Ribbon; } }

        public MainWindow() {
            Instance = this;
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }

        protected override void OnHideDemoModule(DemoModule demo) {
            base.OnHideDemoModule(demo);
            CommonRibbon.ExportGantt = null;
        }
    }
}
