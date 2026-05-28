using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class Table : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.TableReport.Report();
        }
    }
}
