namespace GridDemo {
    public partial class UnboundColumns : GridDemoModule {
        public UnboundColumns() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsUnboundColumns;
        }
    }
}
