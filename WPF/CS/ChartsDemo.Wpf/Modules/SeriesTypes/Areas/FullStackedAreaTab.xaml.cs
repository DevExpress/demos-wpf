namespace ChartsDemo {
    public partial class FullStackedAreaTab : TabItemModule {
        public FullStackedAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
