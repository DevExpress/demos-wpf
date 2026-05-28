namespace ChartsDemo {
    public partial class ScatterLineTab : TabItemModule {
        public ScatterLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
