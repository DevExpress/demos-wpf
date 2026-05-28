namespace GridDemo {
    public partial class GridSummaries : GridDemoModule {
        public GridSummaries() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsGridSummaries;
        }
    }
}
