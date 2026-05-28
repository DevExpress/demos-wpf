namespace ChartsDemo {
    public partial class StackedBarTab : TabItemModule {
        public StackedBarTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
