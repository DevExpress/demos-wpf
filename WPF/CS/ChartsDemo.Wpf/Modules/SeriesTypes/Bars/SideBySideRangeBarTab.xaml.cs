namespace ChartsDemo {
    public partial class SideBySideRangeBarTab : TabItemModule {
        public SideBySideRangeBarTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
