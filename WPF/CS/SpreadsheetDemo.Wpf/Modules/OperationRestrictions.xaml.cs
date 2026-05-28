using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.PropertyGrid;
using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class OperationRestrictions : SpreadsheetDemoModule {
        int optionsGroupCounter = 11;

        public OperationRestrictions() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home;

            propertyGridControl1.SelectedObject = new BehaviorOptionsProvider(spreadsheetControl1.Options.Behavior);
            propertyGridControl1.CustomExpand += OnCustomExpand;
        }

        void OnCustomExpand(object sender, CustomExpandEventArgs args) {
            args.IsExpanded = true;
            args.Handled = true;
            optionsGroupCounter--;
            if(optionsGroupCounter == 0)
                propertyGridControl1.CustomExpand -= OnCustomExpand;
        }
    }
}
