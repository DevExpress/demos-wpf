using DevExpress.DemoData.Models.Vehicles;
using System.Linq;

namespace GridDemo {
    public partial class BandedView : GridDemoModule {
        public BandedView() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsBandedView;
            grid.ItemsSource = new VehiclesContext().Models.ToList();
        }
    }
}
