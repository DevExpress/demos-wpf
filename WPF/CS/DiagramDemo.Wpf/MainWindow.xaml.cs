using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
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
                PreloadCategories.PropertyGrid
            );
        }

        public static MainWindow Instance { get; private set; }
        public static DiagramRibbonControl CommonRibbon { get { return (DiagramRibbonControl)Instance.Ribbon; } }
        public static void InitCommonRibbon(DiagramDesignerControl diagram) {
            DiagramDesignerControl.SetDiagram(CommonRibbon, diagram);
            if(diagram == null) {
                CommonRibbon.Visibility = System.Windows.Visibility.Collapsed;
                MergingProperties.SetElementMergingBehavior(CommonRibbon, ElementMergingBehavior.None);
            }
            else {
                CommonRibbon.Visibility = System.Windows.Visibility.Visible;
                MergingProperties.SetElementMergingBehavior(CommonRibbon, ElementMergingBehavior.InternalWithExternal);
            }
        }

        public MainWindow() {
            Instance = this;
#if NET
            System.Data.Common.DbProviderFactory factory;
            if(!System.Data.Common.DbProviderFactories.TryGetFactory("Microsoft.Data.Sqlite", out factory))
                System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#endif
            InitializeComponent();
        }
    }
}
