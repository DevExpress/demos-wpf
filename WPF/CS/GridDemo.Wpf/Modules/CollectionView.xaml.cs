namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CollectionViewViewModel.(cs)")]
    public partial class CollectionView : GridDemoModule {
        public CollectionView() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsCollectionView;
        }
    }
}
