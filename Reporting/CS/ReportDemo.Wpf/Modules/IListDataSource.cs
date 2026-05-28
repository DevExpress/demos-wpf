using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class IListDataSource : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.IListDataSource.Report();
        }
    }
}
