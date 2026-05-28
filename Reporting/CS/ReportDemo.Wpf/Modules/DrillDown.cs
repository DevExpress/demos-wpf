using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class DrillDown : ReportModuleViewModelBase {
        public override ZoomMode DefaultZoomMode { get { return ZoomMode.ActualSize; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.DrillDownReport.DrillDownReport();
        }
    }
}
