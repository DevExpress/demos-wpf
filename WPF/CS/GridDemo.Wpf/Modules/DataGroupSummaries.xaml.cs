namespace GridDemo {
    public partial class DataGroupSummaries : GridDemoModule {
        public DataGroupSummaries() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsDataGroupSummaries;
        }
    }
}
