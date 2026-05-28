using DevExpress.Mvvm;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class RegularGridDataTab : TabItemModule {
        public RegularGridDataTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart, false, false);
        }
    }


    public class FillStyleItem : BindableBase {
        string title;
        FillStyleBase fillStyle;

        public string Title {
            get { return this.title; }
            set { SetProperty(ref this.title, value, () => Title); }
        }
        public FillStyleBase FillStyle {
            get { return this.fillStyle; }
            set { SetProperty(ref this.fillStyle, value, () => FillStyle); }
        }
    }

}
