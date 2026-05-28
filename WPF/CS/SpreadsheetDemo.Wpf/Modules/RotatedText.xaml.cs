using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class RotatedText : SpreadsheetDemoModule {
        public RotatedText() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home;
        }
    }
}
