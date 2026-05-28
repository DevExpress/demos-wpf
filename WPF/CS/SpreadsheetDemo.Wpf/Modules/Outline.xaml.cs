using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Outline : SpreadsheetDemoModule {
        public Outline() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Data;
        }
    }
}
