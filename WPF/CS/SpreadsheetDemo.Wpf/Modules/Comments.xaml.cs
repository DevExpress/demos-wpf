using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Comments : SpreadsheetDemoModule {
        public Comments() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Review;
        }
    }
}
