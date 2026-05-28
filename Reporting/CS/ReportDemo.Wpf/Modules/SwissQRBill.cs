using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class SwissQRBill : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.SwissQRCode.SwissQRBill();
        }
    }
}
