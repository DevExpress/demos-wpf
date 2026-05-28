using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class HierarchicalReport : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.HierarchicalReport.Report();
        }
    }
}
