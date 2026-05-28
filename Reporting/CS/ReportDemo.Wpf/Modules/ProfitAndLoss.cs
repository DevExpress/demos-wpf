using DevExpress.Xpf.DocumentViewer;
using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class ProfitAndLoss : ReportModuleViewModelBase {
        public override ZoomMode DefaultZoomMode { get { return ZoomMode.FitToWidth; } }
        
        protected override IReport CreateReport() {
            return new XtraReportsDemos.ProfitAndLossReport.Report();
        }
    }
}
