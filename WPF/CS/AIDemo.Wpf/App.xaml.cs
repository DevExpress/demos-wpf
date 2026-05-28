using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace AIDemo {
    public partial class App : Application {
        static App() {
            DemoShellApp.Init(enablePGO:false);
        }
#if DEBUG
        public bool IsDebug { get { return true; } }
#endif
    }
}
