namespace ChartsDemo {
    public partial class PolarLineTab : TabItemModule {
        public PolarLineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
