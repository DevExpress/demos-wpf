using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace WpfDemo {
    public partial class App : Application {
        static App() {
            DemoShellApp.Init();
        }
#if DEBUG
        public bool IsDebug { get { return true; } }
#endif
    }
}
