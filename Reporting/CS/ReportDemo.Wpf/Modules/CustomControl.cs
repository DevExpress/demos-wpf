using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class CustomControl : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.CustomDraw.Report();
        }
    }
}
