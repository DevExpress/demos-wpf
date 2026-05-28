namespace ChartsDemo {
    public partial class ScatterRadarLineTab : TabItemModule {
        public ScatterRadarLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
