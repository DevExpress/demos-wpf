namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataUpdateAnimationViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/StockViewModelBase.(cs)")]
    public partial class DataUpdateAnimation : GridDemoModule {
        public DataUpdateAnimation() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsDataUpdateAnimation;
        }
    }
}
