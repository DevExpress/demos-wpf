namespace GridDemo {
    public partial class CurrentDataRow : GridDemoModule {
        public CurrentDataRow() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsCurrentDataRow;
        }
    }
}
