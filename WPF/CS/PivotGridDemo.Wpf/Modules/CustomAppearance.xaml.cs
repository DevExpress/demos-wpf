namespace PivotGridDemo {
    public partial class CustomAppearance : PivotGridDemoModule {
        public CustomAppearance() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid;
        }
    }
}
