using System;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace PdfViewerDemo.Tests {
    
    public class PdfViewerCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CheckMemoryLeaksWhenFinishingTesting() { return false; }
        protected override bool AllowSwitchToCodeTab(Type moduleType) {
            return false; 
        }
    }
    public class PdfViewerDemoModulesAccessor : DemoModulesAccessor<PdfViewerDemoModule> {
        public PdfViewerDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BasePdfViewerDemoTestingFixture : BaseDemoTestingFixture {
        protected PdfViewerDemoModulesAccessor ModuleAccessor { get; private set; }
        public BasePdfViewerDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract PdfViewerDemoModulesAccessor GetModulesAccessor();
    }
}
