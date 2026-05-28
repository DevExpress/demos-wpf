using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class CalculatedFields : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.CalculatedFieldsReport.Report();
        }
    }
}
