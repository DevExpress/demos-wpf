namespace ChartsDemo {
    public partial class FullStackedBarTab : TabItemModule {
        public FullStackedBarTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
