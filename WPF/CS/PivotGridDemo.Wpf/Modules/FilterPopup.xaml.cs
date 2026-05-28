namespace PivotGridDemo {
    public partial class FilterPopup : PivotGridDemoModule {
        public FilterPopup() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid;
        }
    }
}
