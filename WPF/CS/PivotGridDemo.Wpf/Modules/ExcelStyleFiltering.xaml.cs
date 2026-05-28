using System.Windows;
using System.Windows.Controls;

namespace PivotGridDemo {
    public partial class ExcelStyleFiltering : PivotGridDemoModule {
        public ExcelStyleFiltering() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivot;
            pivot.CollapseAllColumns();
        }
    }
}
