namespace ChartsDemo {
    public partial class ScatterPolarLineTab : TabItemModule {
        public ScatterPolarLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
