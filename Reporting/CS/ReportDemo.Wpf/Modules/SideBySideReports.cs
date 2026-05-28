using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class SideBySideReports : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.SideBySideReports.EmployeeComparisonReport();
        }
    }
}
