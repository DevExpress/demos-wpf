namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PrintMasterDetailViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PrintViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PrintDemoModuleResources.xaml")]
    public partial class PrintingMasterDetail : GridDemoModule {
        public PrintingMasterDetail() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsPrintingMasterDetail;
        }
    }
}
