using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class TipOverTreeLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }

        public TipOverTreeLayoutModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
            diagramControl.LoadDemoData("TipOverTreeLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyTipOverTreeLayout();
        }
    }
}
