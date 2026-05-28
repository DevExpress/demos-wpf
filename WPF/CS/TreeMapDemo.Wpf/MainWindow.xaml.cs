using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace TreeMapDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Charts
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
