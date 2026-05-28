namespace ChartsDemo {
    public partial class StepLineTab : TabItemModule {
        public StepLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
