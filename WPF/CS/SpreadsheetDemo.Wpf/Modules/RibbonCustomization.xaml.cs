using System.Windows;
using DevExpress.Xpf.Core;

namespace SpreadsheetDemo {
    public partial class RibbonCustomization : SpreadsheetDemoModule {
        public RibbonCustomization() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(null);
        }

        private void About_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            ThemedMessageBox.Show(
                "Spreadsheet Ribbon Customization",
                "This demo illustrates how to customize the WPF Spreadsheet's integrated ribbon UI.\n\nSwitch to the Code View to learn how to use the Spreadsheet Control's RibbonActions collection to create, remove or modify ribbon elements.",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
