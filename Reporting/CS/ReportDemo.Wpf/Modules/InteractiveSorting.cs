using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class InteractiveSorting : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.InteractiveSorting.Report();
        }
    }
}
