namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/VerticalScrollingOptionsViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/VerticalScrollingOptionsTemplates.xaml")]
    public partial class VerticalScrollingOptions : GridDemoModule {
        public VerticalScrollingOptions() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsVerticalScrollingOptions;
        }
    }
}
