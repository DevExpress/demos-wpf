using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class BarCodes : ReportModuleViewModelBase {
        protected override IReport CreateReport() {
            return new XtraReportsDemos.BarCodes.BarCodeTypesReport();
        }
    }
}
