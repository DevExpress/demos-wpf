namespace ChartsDemo {
    public partial class GridPaneLayoutTab : TabItemModule {
        public GridPaneLayoutTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
