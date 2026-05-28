using System;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace SchedulingDemo.Tests {
    public class SchedulingCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CanRunModule(Type moduleType) {
            if(moduleType == typeof(OnDemandDataLoading))
                return false;
            return base.CanRunModule(moduleType);
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            if(moduleType == typeof(PrintingTemplatesDemoModule) || moduleType == typeof(WindowsCustomizationDemoModule))
                return false;
            return true;
        }
    }
}
