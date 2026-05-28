namespace GridDemo {
    public partial class MasterDetailView : GridDemoModule {
        public MasterDetailView() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsMasterDetailView;
        }
    }
}
