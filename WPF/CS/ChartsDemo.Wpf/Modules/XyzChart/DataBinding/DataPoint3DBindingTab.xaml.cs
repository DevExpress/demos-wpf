namespace ChartsDemo {
    public partial class DataPoint3DBindingTab : TabItemModule {
        public DataPoint3DBindingTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }
    }
}
