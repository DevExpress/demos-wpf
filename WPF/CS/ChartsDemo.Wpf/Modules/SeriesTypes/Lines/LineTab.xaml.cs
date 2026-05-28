namespace ChartsDemo {
    public partial class LineTab : TabItemModule {
        public LineTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
