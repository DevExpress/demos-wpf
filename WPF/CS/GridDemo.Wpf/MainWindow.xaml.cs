using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
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
                PreloadCategories.Printing,
                PreloadCategories.RichEdit,
                PreloadCategories.Scheduling,
                PreloadCategories.Charts,
                PreloadCategories.Spreadsheet
            );
        }

        public static MainWindow Instance { get; private set; }
        public static GridDemoRibbon CommonRibbon { get { return (GridDemoRibbon)Instance.Ribbon; } }

        public MainWindow() {
            Instance = this;
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
#if NET
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#endif
        }
        protected override void OnHideDemoModule(DemoModule demo) {
            base.OnHideDemoModule(demo);
            CommonRibbon.ExportGrid = null;
            CommonRibbon.ReportService = null;
        }
    }
}
