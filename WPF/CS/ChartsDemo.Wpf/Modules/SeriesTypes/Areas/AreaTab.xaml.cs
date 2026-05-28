namespace ChartsDemo {
    public partial class AreaTab : TabItemModule {
        public AreaTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
