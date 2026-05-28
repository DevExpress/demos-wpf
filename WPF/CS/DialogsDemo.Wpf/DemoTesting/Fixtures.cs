using DevExpress.Xpf.DemoBase.DemoTesting;

namespace DialogsDemo.Tests {
    public class DialogsCheckAllDemosFixture : CheckAllDemosFixture { }
    public class DialogsDemoModulesAccessor : DemoModulesAccessor<DialogsDemoModule> {
        public DialogsDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseDialogsDemoTestingFixture : BaseDemoTestingFixture {
        protected DialogsDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseDialogsDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract DialogsDemoModulesAccessor GetModulesAccessor();
    }
}
