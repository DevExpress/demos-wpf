namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiThreadDataProcessingViewModel.(cs)")]
    public partial class MultiThreadDataProcessing : GridDemoModule {
        public MultiThreadDataProcessing() {
            InitializeComponent();
            MainWindow.CommonRibbon.ReportService = rmsMultiThreadDataProcessing;
            ModuleUnloaded += (s, e) => {
                grid.ItemsSource = null;
                pLinqInstantSource.Dispose();
            };
        }
    }
}
