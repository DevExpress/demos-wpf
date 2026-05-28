namespace GridDemo {
    public partial class DataDependentDetails : GridDemoModule {
        public DataDependentDetails() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsDataDependentDetails;
        }
    }
}
