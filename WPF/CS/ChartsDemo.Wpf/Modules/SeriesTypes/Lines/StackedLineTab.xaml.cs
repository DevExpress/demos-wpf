namespace ChartsDemo {
    public partial class StackedLineTab : TabItemModule {
        public StackedLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
