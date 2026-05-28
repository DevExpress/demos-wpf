using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class TrendAnalysis : SpreadsheetDemoModule {
        public TrendAnalysis() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Formulas;
        }
    }
}
