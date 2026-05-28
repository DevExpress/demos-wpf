using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace LayoutControlDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadData(false, true);
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.Ribbon,
                PreloadCategories.LayoutControl,
                PreloadCategories.Docking,
                PreloadCategories.Accordion
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
