namespace ChartsDemo {
    public partial class RadarPointTab : TabItemModule {
        public RadarPointTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
