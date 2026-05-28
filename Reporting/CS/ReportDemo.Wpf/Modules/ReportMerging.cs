using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class ReportMerging : ReportModuleViewModelBase {
        public override PageDisplayMode PageDisplayMode { get { return PageDisplayMode.Columns; } }
        public override int PageColumnsCount { get { return 3; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.ReportMerging.MergedReport();
        }
    }
}
