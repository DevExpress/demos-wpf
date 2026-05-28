using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.XtraReports.UI;

namespace ReportWpfDemo {
    public class ReportDesignerViewModel : ImmutableObject {
        public XtraReport Report { get; private set; }
        public ICommand ReportSavedCommand { get; private set; }
        public bool Saved { get; private set; }

        public ReportDesignerViewModel(XtraReport report) {
            Guard.ArgumentNotNull(report, "report");
            ReportSavedCommand = new DelegateCommand(OnReportSaved);
            Report = report;
        }

        void OnReportSaved() {
            Saved = true;
        }
    }
}
