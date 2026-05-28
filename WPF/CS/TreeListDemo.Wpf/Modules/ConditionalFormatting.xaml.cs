using System.Collections.Generic;

namespace TreeListDemo {
    public partial class ConditionalFormatting : TreeListDemoModule {
        public ConditionalFormatting() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
    }

    public class SalesDataViewModel {
        public SalesDataViewModel() {
            SalesData = SalesDataGenerator.CreateData();
        }
        public IList<SalesData> SalesData { get; private set; }
    }
}
