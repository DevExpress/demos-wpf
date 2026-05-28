using System;

namespace TreeListDemo {
    public partial class NewItemRow : TreeListDemoModule {
        public NewItemRow() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
        void OnInitNewNode(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs e) {
            view.SetNodeValue(e.Node, "ID", view.TotalNodesCount + 1);
            view.SetNodeValue(e.Node, "StartDate", DateTime.Now);
            view.SetNodeValue(e.Node, "DueDate", DateTime.Now);
        }
    }
}
