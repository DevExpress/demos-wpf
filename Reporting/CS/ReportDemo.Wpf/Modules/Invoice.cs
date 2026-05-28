using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class Invoice : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.NorthwindTraders.InvoiceReport();
        }
    }
}
