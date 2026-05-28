using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace DockingDemo {
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
                PreloadCategories.PropertyGrid,
                PreloadCategories.Map
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
