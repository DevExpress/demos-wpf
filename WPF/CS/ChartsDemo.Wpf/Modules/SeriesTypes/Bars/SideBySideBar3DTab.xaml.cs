namespace ChartsDemo {
    public partial class SideBySideBar3DTab : TabItemModule {
        public SideBySideBar3DTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }

        bool isAnimationCompleted = false;
        public override bool IsAnimationCompleted {
            get {
                return isAnimationCompleted;
            }
        }
        private void Storyboard_Completed(object sender, System.EventArgs e) {
            isAnimationCompleted = true;
        }
    }
}
