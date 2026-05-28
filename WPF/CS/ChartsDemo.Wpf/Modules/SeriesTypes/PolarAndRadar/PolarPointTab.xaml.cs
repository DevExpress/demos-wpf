namespace ChartsDemo {
    public partial class PolarPointTab : TabItemModule {
        public PolarPointTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
