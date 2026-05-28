namespace ChartsDemo {
    public partial class StackedSplineAreaTab : TabItemModule {
        public StackedSplineAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
