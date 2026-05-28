namespace PivotGridDemo {
    public partial class ConditionalFormatting : PivotGridDemoModule {
        public ConditionalFormatting() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid;
        }        
    }
}
