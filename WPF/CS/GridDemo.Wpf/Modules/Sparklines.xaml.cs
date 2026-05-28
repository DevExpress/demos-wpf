using DevExpress.Xpf.Grid.Automation;

namespace GridDemo {
    public partial class Sparklines : GridDemoModule {
        public Sparklines() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
        }

        private void TableView_AutomationRequested(object sender, System.Windows.RoutedEventArgs e) {
            CellAutomationEventArgs cellArgs = e as CellAutomationEventArgs;
            if(cellArgs != null && cellArgs.Column.FieldName == "ChartSource") {
                cellArgs.AutomationValue = "Line chart";
            }
        }
    }
}
