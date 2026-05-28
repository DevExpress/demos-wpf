namespace ChartsDemo {
    public partial class SplineTab : TabItemModule {
        public SplineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
