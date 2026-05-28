namespace GridDemo {
    public partial class FixedDataColumns : GridDemoModule {
        public FixedDataColumns() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsFixedDataColumns;
        }
    }
}
