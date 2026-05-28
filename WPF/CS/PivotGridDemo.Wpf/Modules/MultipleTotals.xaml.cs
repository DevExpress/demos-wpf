namespace PivotGridDemo {
    public partial class MultipleTotals : PivotGridDemoModule {

        public MultipleTotals() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid;
        }
    }
}
