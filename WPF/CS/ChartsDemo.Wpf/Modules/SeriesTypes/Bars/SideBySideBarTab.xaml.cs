namespace ChartsDemo {
    public partial class SideBySideBarTab : TabItemModule {
        public SideBySideBarTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
