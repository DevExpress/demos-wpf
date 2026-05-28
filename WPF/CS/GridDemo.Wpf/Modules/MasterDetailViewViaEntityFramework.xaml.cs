using DevExpress.DemoData.Models;
#if NET
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif

namespace GridDemo {
    public partial class MasterDetailViewViaEntityFramework : GridDemoModule {
        public MasterDetailViewViaEntityFramework() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
            MainWindow.CommonRibbon.ReportService = rmsMasterDetailViewViaEntityFramework;
            var customers = new NWindContext().Customers;
            customers.Load();
            grid.ItemsSource = customers.Local;
        }
    }
}
