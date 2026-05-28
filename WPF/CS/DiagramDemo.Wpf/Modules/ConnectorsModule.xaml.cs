namespace DiagramDemo {
    public partial class ConnectorsModule : DiagramDemoModule {
        public ConnectorsModule() {
            InitializeComponent();
            MainWindow.InitCommonRibbon(diagramControl);
        }
    }
}
