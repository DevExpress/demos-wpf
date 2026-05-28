using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace PdfViewerDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Grid
            );
        }
        public MainWindow() {
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }
    }
}
