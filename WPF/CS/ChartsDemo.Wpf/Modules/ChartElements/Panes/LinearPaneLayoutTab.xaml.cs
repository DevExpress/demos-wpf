namespace ChartsDemo {
    public partial class LinearPaneLayoutTab : TabItemModule {
        public LinearPaneLayoutTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
