using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class SortAndFilter : SpreadsheetDemoModule {
        public SortAndFilter() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Data;
        }
    }
}
