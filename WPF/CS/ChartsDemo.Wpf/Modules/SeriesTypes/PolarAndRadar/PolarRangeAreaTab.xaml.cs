namespace ChartsDemo {
    public partial class PolarRangeAreaTab : TabItemModule {
        public PolarRangeAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
