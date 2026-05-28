using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class TreeLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }

        public TreeLayoutModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
            diagramControl.LoadDemoData("TreeLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyTreeLayout();
        }
    }
}
