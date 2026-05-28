using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class ProductList : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.NorthwindTraders.ProductListReport();
        }
    }
}
