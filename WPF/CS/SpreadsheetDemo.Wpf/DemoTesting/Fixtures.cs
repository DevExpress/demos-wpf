using System;
using System.Linq;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace SpreadsheetDemo.Tests {
    public class SpreadsheetCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CanRunModule(Type moduleType) {
#if !NET
            if(moduleType == typeof(BindingToDataSource))
                return false;
#endif
            return base.CanRunModule(moduleType);
        }
    }
    public class SpreadsheetDemoModulesAccessor : DemoModulesAccessor<SpreadsheetDemoModule> {
        public SpreadsheetDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseSpreadsheetDemoTestingFixture : BaseDemoTestingFixture {
        protected SpreadsheetDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseSpreadsheetDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract SpreadsheetDemoModulesAccessor GetModulesAccessor();
    }
}
