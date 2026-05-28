using DevExpress.Xpf.DemoBase.DemoTesting;

namespace MVVMDemo.Tests {
    public class MVVMCheckAllDemosFixture : CheckAllDemosFixture { }
    public class MVVMDemoModulesAccessor : DemoModulesAccessor<MVVMDemoModule> {
        public MVVMDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseMVVMDemoTestingFixture : BaseDemoTestingFixture {
        protected MVVMDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseMVVMDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract MVVMDemoModulesAccessor GetModulesAccessor();
    }
}
