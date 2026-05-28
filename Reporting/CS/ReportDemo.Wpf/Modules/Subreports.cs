using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class Subreports : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.Subreports.MasterReport();
        }
    }
}
