namespace ChartsDemo {
    public partial class OverlappedRangeBarTab : TabItemModule {
        public OverlappedRangeBarTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
