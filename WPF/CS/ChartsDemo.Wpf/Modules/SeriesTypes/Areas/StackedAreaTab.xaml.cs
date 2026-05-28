namespace ChartsDemo {
    public partial class StackedAreaTab : TabItemModule {
        public StackedAreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
