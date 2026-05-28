namespace ChartsDemo {
    public partial class RangeAreaTab : TabItemModule {
        public RangeAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
