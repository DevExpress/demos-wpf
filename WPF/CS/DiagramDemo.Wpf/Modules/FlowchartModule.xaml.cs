namespace DiagramDemo {
    public partial class FlowchartModule : DiagramDemoModule {
        public FlowchartModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
            diagramControl.LoadDemoData("Flowchart.xml");
        }
    }
}
