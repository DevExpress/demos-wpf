namespace ChartsDemo {
    public partial class SurfaceSeriesViewTab : TabItemModule {
        public SurfaceSeriesViewTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
