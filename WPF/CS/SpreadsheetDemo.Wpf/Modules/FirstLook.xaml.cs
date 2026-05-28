using System.Windows;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class FirstLook : SpreadsheetDemoModule {
        public FirstLook() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(spreadsheetControl1);
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home;
        }

        private void spreadsheetControl1_InvalidFormatException(object sender, SpreadsheetInvalidFormatExceptionEventArgs e) {
            ThemedMessageBox.Show(
                "Error",
                string.Format("Cannot open the file '{0}' because the file format or file extension is not valid.\n" +
                "Verify that file has not been corrupted and that the file extension matches the format of the file.", e.SourceUri),
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void spreadsheetControl1_DocumentClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if(spreadsheetControl1.Modified) {
                string currentFileName = spreadsheetControl1.Options.Save.CurrentFileName;
                string message = !string.IsNullOrEmpty(currentFileName) ?
                    string.Format("Do you want to save the changes you made for '{0}'?", currentFileName) :
                    "Do you want to save the changes?";
                MessageBoxResult result = ThemedMessageBox.Show("Warning", message, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                    spreadsheetControl1.SaveDocument();
                e.Cancel = result == MessageBoxResult.Cancel;
            }
        }
    }
}
