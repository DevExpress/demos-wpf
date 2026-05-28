using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class ShrinkGrow : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.ShrinkGrow.Report();
        }
    }
}
