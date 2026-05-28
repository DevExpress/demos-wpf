namespace ChartsDemo {
    public partial class SplineAreaTab : TabItemModule {
        public SplineAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
