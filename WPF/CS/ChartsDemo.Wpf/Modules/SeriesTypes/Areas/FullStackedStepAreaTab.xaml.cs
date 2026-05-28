namespace ChartsDemo {
    public partial class FullStackedStepAreaTab : TabItemModule {
        public FullStackedStepAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
