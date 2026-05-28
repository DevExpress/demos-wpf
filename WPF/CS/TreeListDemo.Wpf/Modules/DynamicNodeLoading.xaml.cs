using DevExpress.Xpf.DemoBase;

namespace TreeListDemo {
    [CodeFile("Data/DynamicNodeLoadingViewModel.(cs)")]
    [CodeFile("Data/DynamicNodeLoadingBehavior.(cs)")]
    [CodeFile("Data/FileSystemData.(cs)")]
    public partial class DynamicNodeLoading : TreeListDemoModule {
        public DynamicNodeLoading() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
    }
}
