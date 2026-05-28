using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Charting : SpreadsheetDemoModule {
        public Charting() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Insert;
        }
    }
}
