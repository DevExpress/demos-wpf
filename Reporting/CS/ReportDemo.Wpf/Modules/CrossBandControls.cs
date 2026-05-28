using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class CrossBandControls : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.CrossBandControls.Report();
        }
    }
}
