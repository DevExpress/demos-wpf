using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class MasterDetail : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.MasterDetailReport.Report();
        }
    }
}
