namespace ChartsDemo {
    public partial class FunnelTab : TabItemModule {
        public FunnelTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
