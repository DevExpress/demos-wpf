using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class PivotGridAndChart : ReportModuleViewModelBase {
        public override ZoomMode DefaultZoomMode { get { return ZoomMode.PageLevel; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.PivotGridAndChart.Report();
        }
    }
}
