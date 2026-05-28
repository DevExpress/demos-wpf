using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class CircularLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }

        public CircularLayoutModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
            diagramControl.LoadDemoData("CircularLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyCircularLayout();
        }
    }
}
