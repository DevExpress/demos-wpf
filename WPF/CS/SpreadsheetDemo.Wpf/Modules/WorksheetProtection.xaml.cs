using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class WorksheetProtection : SpreadsheetDemoModule {
        public WorksheetProtection() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Review;
        }
    }
}
