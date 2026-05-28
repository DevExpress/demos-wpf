using DevExpress.XtraReports;

namespace ReportWpfDemo {
    public class CrossBandContent : ReportModuleViewModelBase {
        public override bool ShowPageMargins { get { return false; } }

        protected override IReport CreateReport() {
            return new XtraReportsDemos.CrossBandContent.Report();
        }
    }
}
