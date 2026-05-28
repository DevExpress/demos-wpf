using System.ComponentModel.DataAnnotations;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CustomerReportsViewModel.(cs)")]
    public partial class CustomerReports : PivotGridDemoModule {
        public CustomerReports() {
            InitializeComponent();
            reportsList.EditValueChanged += OnReportsListEditValueChanged;
        }
        void OnReportsListEditValueChanged(object sender, EditValueChangedEventArgs e) {
            var value = e.NewValue?.ToString();
            if(string.IsNullOrEmpty(value)) {
                MainWindow.CommonRibbon.ExportPivotGrid = null;
                return;
            }
            var pivot = PivotGridHolder.FindName(value + "PivotGrid") as PivotGridControl;
            MainWindow.CommonRibbon.ExportPivotGrid = pivot;
        }
    }

    public enum CustomerReportType {
        Customers,
        [Display(Name = "Products (filtering)")]
        Products,
        [Display(Name = "Top 2 Products")]
        Top2Products,
        [Display(Name = "Top 10 Customers")]
        Top10Customers
    }

}
