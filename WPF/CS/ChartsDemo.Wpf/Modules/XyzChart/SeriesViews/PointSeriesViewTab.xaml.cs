namespace ChartsDemo {
    public partial class PointSeriesViewTab : TabItemModule {
        public PointSeriesViewTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
