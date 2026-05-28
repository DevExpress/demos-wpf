using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace DialogsDemo {
    public partial class MainWindow : DemoShellWindow {
        static MainWindow() {
            DemoPreloadHelper.PreloadThemeAsync(
                PreloadCategories.Core,
                PreloadCategories.LayoutControl,
                PreloadCategories.Ribbon,
                PreloadCategories.Docking,
                PreloadCategories.Accordion,
                PreloadCategories.Grid,
                PreloadCategories.Dialogs
            );
        }
        public MainWindow() {
            InitializeComponent();
        }
    }
}
