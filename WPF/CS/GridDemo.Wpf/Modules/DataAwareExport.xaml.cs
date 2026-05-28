namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataAwareExport/ProductSaleData.cs")]
    public partial class DataAwareExport : GridDemoModule {
        public DataAwareExport() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsDataAwareExport;
        }
    }
}
