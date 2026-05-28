using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class Sparkline : ReportModuleViewModelBase {
        public override ZoomMode DefaultZoomMode { get { return ZoomMode.ActualSize; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.Sparkline.Report();
        }
    }
}
