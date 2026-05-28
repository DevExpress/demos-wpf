using DevExpress.Xpf.DemoBase;

namespace TreeListDemo {
    [CodeFile("Data/SmartColumnsGenerationViewModel.(cs)")]
    public partial class SmartColumnsGeneration : TreeListDemoModule {
        public SmartColumnsGeneration() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
    }
}
