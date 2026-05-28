using System.IO;
using DevExpress.DemoData.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public class DrillDownViewModel {

        public DrillDownViewModel() {
            CanPerformDrillDown = true;
        }

        public bool CanPerformDrillDown { get; set; }

        public bool CanShowDrillDown(CellInfo cellInfo) {
            return CanPerformDrillDown && cellInfo != null;
        }
        public void ShowDrillDown(CellInfo cellInfo) {
            this.GetService<IDialogService>("DrillDownTemplate").ShowDialog(MessageButton.OK, "Drill Down Results", cellInfo);
        }

    }
}
