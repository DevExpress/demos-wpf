namespace ChartsDemo {
    public partial class PointTab : TabItemModule {
        public PointTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
