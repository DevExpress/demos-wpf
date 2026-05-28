using System.Linq;

namespace TreeListDemo {
    public partial class NodeSummaries : TreeListDemoModule {
        public NodeSummaries() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
        private void OnShowingNodeFooter(object sender, DevExpress.Xpf.Grid.TreeList.TreeListShowingNodeFooterEventArgs e) {
            if(!chkHideSingleNodeFooters.IsChecked.Value) return;
            if(e.IsRootNode || e.Node.Nodes.Where(node => node.IsVisible).Count() == 1)
                e.Allow = false;
        }
    }
}
