using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace GaugesDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Gauges,
                PreloadCategories.Charts
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
