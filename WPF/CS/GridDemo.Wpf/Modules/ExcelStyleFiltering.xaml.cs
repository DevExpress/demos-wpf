using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    public partial class ExcelStyleFiltering : GridDemoModule {
        public ExcelStyleFiltering() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsExcelStyleFiltering;
            grid.ItemsSource = OrderDataGenerator.GenerateVehicleOrders(10000);
        }
    }
}
