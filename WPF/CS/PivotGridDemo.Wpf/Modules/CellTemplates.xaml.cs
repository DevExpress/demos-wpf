namespace PivotGridDemo {
    public partial class CellTemplates : PivotGridDemoModule {
        public CellTemplates() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid;
        }
    }

    public enum CellTemplateType { None, ShareOnly, ValueAndShare }
}
