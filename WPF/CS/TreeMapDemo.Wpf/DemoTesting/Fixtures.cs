using System;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace TreeMapDemo.Tests {
    public class TreeMapCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, ITheme theme) {
            return false;
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return false; 
        }
    }
}
