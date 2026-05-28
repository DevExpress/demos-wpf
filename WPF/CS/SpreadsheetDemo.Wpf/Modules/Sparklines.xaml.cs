using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Sparklines : SpreadsheetDemoModule {
        public Sparklines() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home;
        }
    }
}
