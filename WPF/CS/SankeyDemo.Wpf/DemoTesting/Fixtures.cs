using System;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace SankeyDemo.Tests {
    public class SankeyCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, ITheme theme) {
            return false;
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return false;
        }
    }
}
