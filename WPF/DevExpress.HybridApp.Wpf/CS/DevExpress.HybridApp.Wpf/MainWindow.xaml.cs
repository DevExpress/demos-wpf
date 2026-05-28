using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;

namespace DevExpress.DevAV {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            App.Test(this);
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
            InitializeComponent();
            if(Height > SystemParameters.VirtualScreenHeight || Width > SystemParameters.VirtualScreenWidth)
                WindowState = WindowState.Maximized;
            EventManager.RegisterClassHandler(typeof(AppBarButton), AppBarButton.ClickEvent, new RoutedEventHandler(OnAppBarButtonClick), true);
        }
        void OnAppBarButtonClick(object sender, RoutedEventArgs e) {
            var barButton = (AppBarButton)sender;
            var content = barButton.Label ?? barButton.Name;
            if(content != null) {
                Logger.Log(string.Format("HybridApp: BarButtonClick: {0}", content.ToString()));
            }
        }
    }
}
