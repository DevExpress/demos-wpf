using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class DataValidation : SpreadsheetDemoModule {
        public DataValidation() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Data;
        }
    }
}
