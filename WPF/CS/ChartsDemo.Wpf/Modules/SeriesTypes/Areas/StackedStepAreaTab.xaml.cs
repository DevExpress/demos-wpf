namespace ChartsDemo {
    public partial class StackedStepAreaTab : TabItemModule {
        public StackedStepAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
