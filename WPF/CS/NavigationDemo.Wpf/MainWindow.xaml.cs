using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace NavigationDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadData(false, true);
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid,
                PreloadCategories.PdfViewer
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
