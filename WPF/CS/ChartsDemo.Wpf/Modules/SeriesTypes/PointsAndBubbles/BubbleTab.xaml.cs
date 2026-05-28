namespace ChartsDemo {
    public partial class BubbleTab : TabItemModule {
        public BubbleTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
