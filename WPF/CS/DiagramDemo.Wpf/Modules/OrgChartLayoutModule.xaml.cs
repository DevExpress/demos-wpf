using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class OrgChartLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }
        public OrgChartLayoutModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
            diagramControl.LoadDemoData("OrgChartLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyOrgChartLayout();
        }
    }
}
