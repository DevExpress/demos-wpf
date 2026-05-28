using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Printing;
using DevExpress.XtraReports.Parameters.ViewModels;

namespace ReportWpfDemo {
    [TargetType(typeof(DocumentPreviewControl))]
    public class ParametersModelProvider : ServiceBase {
        public ParameterPanelViewModel ParametersModel { 
            get {
                return IsAttached
                    ? (AssociatedObject as DocumentPreviewControl).With(x => x.ParameterPanelViewModel)
                    : null;
            }
        }
    }
}
