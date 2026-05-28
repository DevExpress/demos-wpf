using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class VehicleInspectionReport : ReportModuleViewModelBase {
        public override ZoomMode DefaultZoomMode { get { return ZoomMode.PageLevel; } }
        public override PageDisplayMode PageDisplayMode { get { return PageDisplayMode.Columns; } }
        public override int PageColumnsCount { get { return 2; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.VehicleInspectionReport.Report();
        }
    }
}
