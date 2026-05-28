using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace PropertyGridDemo {
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
                PreloadCategories.Charts
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
