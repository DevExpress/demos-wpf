namespace ChartsDemo {
    public partial class StackedArea3DTab : TabItemModule {
        public StackedArea3DTab() {
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
