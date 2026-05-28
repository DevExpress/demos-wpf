using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace WindowsUIDemo {
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
                PreloadCategories.RichEdit
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
