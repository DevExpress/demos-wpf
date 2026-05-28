using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;

namespace TreeListDemo {
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
                PreloadCategories.RichEdit,
                PreloadCategories.Spreadsheet,
                PreloadCategories.Printing
            );
        }

        public static MainWindow Instance { get; private set; }
        public static void InitRibbon(TreeListControl treeList) {
            var ribbon = (TreeListDemoRibbon)Instance.Ribbon;
            ribbon.ExportTreeList = treeList;
        }

        public MainWindow() {
            Instance = this;
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }
    }
}
