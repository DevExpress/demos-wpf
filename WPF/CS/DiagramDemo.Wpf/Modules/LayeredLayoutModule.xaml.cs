using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class LayeredLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }

        public LayeredLayoutModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
            diagramControl.LoadDemoData("LayeredLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplySugiyamaLayout();
        }
    }
}
