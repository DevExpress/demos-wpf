namespace ChartsDemo {
    public partial class Bubble3DSeriesViewTab : TabItemModule {
        public Bubble3DSeriesViewTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
