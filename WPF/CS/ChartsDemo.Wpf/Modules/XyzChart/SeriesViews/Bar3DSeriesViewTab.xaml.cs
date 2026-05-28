namespace ChartsDemo {
    public partial class Bar3DSeriesViewTab : TabItemModule {
        public Bar3DSeriesViewTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
