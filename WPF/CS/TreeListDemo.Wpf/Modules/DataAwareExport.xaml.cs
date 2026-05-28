using System.Windows;
using GridDemo;

namespace TreeListDemo {
    public partial class DataAwareExport : TreeListDemoModule {
        public DataAwareExport() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
        void ExportButtonClick(object sender, RoutedEventArgs e) {
            DemoModuleExportHelper.ExportToXlsx(view);
        }
    }
}
