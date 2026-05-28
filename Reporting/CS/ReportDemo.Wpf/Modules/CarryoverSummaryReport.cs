using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class CarryoverSummaryReport : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.CarryoverSummaryReport.Report();
        }
    }
}
