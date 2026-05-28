namespace ChartsDemo {
    public partial class FullStackedLineTab : TabItemModule {
        public FullStackedLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
