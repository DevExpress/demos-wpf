using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace PivotGridDemo {
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
                PreloadCategories.Charts,
                PreloadCategories.Printing
            );
        }

        public static MainWindow Instance { get; private set; }
        public static PivotGridDemoRibbon CommonRibbon { get { return (PivotGridDemoRibbon)Instance.Ribbon; } }

        public MainWindow() {
            Instance = this;
#if NET
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#endif
            InitializeComponent();
        }
        protected override void OnHideDemoModule(DemoModule demo) {
            base.OnHideDemoModule(demo);
            CommonRibbon.ExportPivotGrid = null;
        }
    }
}
