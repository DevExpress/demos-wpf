using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
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
                PreloadCategories.PropertyGrid,
                PreloadCategories.Charts,
                PreloadCategories.Scheduling
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
