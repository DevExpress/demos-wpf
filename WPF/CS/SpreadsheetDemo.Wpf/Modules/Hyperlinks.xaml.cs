using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Hyperlinks : SpreadsheetDemoModule {
        public Hyperlinks() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Insert;
        }
    }
}
