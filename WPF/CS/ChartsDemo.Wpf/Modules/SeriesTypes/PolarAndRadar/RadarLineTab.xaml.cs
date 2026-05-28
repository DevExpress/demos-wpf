namespace ChartsDemo {
    public partial class RadarLineTab : TabItemModule {
        public RadarLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
