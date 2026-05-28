namespace ChartsDemo {
    public partial class PolarAreaTab : TabItemModule {
        public PolarAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
