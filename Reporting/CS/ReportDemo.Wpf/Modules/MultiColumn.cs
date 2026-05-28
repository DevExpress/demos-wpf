using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class MultiColumn : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.MultiColumnReport.Report();
        }
    }
}
