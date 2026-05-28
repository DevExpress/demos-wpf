using DevExpress.Xpf.DemoBase.DemoTesting;

namespace DiagramDemo.Tests {
    public class DiagramCheckAllDemosFixture : CheckAllDemosFixture { }
    public class DiagramDemoModulesAccessor : DemoModulesAccessor<DiagramDemoModule> {
        public DiagramDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseDiagramDemoTestingFixture : BaseDemoTestingFixture {
        protected DiagramDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseDiagramDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract DiagramDemoModulesAccessor GetModulesAccessor();
    }
}
