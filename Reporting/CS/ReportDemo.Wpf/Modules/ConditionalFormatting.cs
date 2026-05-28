using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class ConditionalFormatting : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.FormattingRules.Report();
        }
    }
}
