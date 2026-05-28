namespace ChartsDemo {
    public partial class RadarRangeAreaTab : TabItemModule {
        public RadarRangeAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
