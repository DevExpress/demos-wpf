namespace GridDemo {
    public partial class StandardTableView : GridDemoModule {
        public StandardTableView() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsStandardTableView;
        }
    }
}
