namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FixedRowsViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/StockViewModelBase.(cs)")]
    public partial class FixedRows : GridDemoModule {
        public FixedRows() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsFixedRows;
        }
    }
}
