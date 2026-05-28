using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace RibbonDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
