using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Reports.UserDesigner.Extensions;

namespace GridDemo {
    public partial class GridDemoRibbon : UserControl {
        public static readonly DependencyProperty ExportGridProperty =
             DependencyProperty.Register(nameof(ExportGrid), typeof(GridControl), typeof(GridDemoRibbon), new PropertyMetadata(null));
        public static readonly DependencyProperty ReportServiceProperty =
            DependencyProperty.Register(nameof(ReportService), typeof(GridReportManagerService), typeof(GridDemoRibbon), new PropertyMetadata(null));

        public GridControl ExportGrid { get { return (GridControl)GetValue(ExportGridProperty); } set { SetValue(ExportGridProperty, value); } }
        public GridReportManagerService ReportService { get { return (GridReportManagerService)GetValue(ReportServiceProperty); } set { SetValue(ReportServiceProperty, value); } }

        public GridDemoRibbon() {
            DataContext = this;
            InitializeComponent();
        }
    }
}
