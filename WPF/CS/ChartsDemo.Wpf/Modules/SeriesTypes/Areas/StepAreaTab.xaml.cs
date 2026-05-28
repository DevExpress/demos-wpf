namespace ChartsDemo {
    public partial class StepAreaTab : TabItemModule {
        public StepAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
