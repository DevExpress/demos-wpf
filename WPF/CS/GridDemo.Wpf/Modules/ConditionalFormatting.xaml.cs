namespace GridDemo {
    public partial class ConditionalFormatting : GridDemoModule {
        public ConditionalFormatting() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsConditionalFormatting;
        }
    }
}
