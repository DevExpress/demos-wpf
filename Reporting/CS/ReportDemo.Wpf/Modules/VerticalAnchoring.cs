using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class VerticalAnchoring : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.AnchorVertical.Report();
        }
    }
}
