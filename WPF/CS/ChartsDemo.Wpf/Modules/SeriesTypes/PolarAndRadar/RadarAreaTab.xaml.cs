namespace ChartsDemo {
    public partial class RadarAreaTab : TabItemModule {
        public RadarAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
