using System;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.Native;

namespace ReportWpfDemo.Tests {
    public class ReportsCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToCodeTab(Type moduleType) {
            return false;
        }

        protected override bool SwitchAllThemes(Type moduleType) {
            return false;
        }

        protected override int TestingThemesCount() {
            return 5;
        }

        protected override bool CanRunModule(Type moduleType) {
            if(moduleType == typeof(LargeQuantityOfData))
                return false;
            return base.CanRunModule(moduleType);
        }
    }
}
