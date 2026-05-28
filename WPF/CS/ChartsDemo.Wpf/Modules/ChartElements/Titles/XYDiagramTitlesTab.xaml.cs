using DevExpress.XtraPrinting.Native;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace ChartsDemo {
    public partial class XYDiagramTitlesTab : TabItemModule {
        public XYDiagramTitlesTab() {
            InitializeComponent();
        }
        protected override void OnSelected() {
            base.OnSelected();
            MainWindow.CommonRibbon.Init(chart);
        }


        void Hyperlink_Click(object sender, RoutedEventArgs e) {
            Hyperlink source = sender as Hyperlink;
            if (source != null) {
                ProcessLaunchHelper.StartConfirmed(source.NavigateUri.ToString());
            }
        }
    }
}
