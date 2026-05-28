using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Services;

namespace SpreadsheetDemo {
    public partial class HeaderFooter : SpreadsheetDemoModule {
        public HeaderFooter() {
            InitializeComponent();
            MainWindow.CommonRibbon.Init(null);
        }

        private void btnPageSetup_Click(object sender, System.Windows.RoutedEventArgs e) {
            ISpreadsheetCommandFactoryService service = (ISpreadsheetCommandFactoryService)spreadsheetControl1.GetService(typeof(ISpreadsheetCommandFactoryService));
            SpreadsheetCommand command = service.CreateCommand(SpreadsheetCommandId.PageSetupHeaderFooter);
            command.ForceExecute(command.CreateDefaultCommandUIState());
        }

        private void btnPrintPreview_Click(object sender, System.Windows.RoutedEventArgs e) {
            ISpreadsheetCommandFactoryService service = (ISpreadsheetCommandFactoryService)spreadsheetControl1.GetService(typeof(ISpreadsheetCommandFactoryService));
            SpreadsheetCommand command = service.CreateCommand(SpreadsheetCommandId.FilePrintPreview);
            command.ForceExecute(command.CreateDefaultCommandUIState());
        }
    }
}
