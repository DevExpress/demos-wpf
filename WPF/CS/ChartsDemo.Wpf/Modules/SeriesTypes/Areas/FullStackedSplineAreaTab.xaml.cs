namespace ChartsDemo {
    public partial class FullStackedSplineAreaTab : TabItemModule {
        public FullStackedSplineAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
