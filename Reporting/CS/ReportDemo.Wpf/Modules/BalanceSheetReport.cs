using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class BalanceSheetReport : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.BalanceSheetReport.Report();
        }
    }
}
