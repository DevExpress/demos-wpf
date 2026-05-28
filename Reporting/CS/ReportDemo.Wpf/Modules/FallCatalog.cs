using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class FallCatalog : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.NorthwindTraders.CatalogReport();
        }
    }
}
