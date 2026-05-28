using System.Threading;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace NavigationDemo.Tests {
    public class AccordionCheckAllDemosFixture : CheckAllDemosFixture {
        protected override void CreateCheckOptionsAction() {
            base.CreateCheckOptionsAction();
            if(DemoBaseTesting.CurrentDemoModule.GetType() == typeof(NavigationPaneDemoModule))
                Thread.Sleep(4000);
        }
    }
    public class AccordionDemoModulesAccessor : DemoModulesAccessor<AccordionDemoModule> {
        public AccordionDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseAccordionDemoTestingFixture : BaseDemoTestingFixture {
        protected AccordionDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseAccordionDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract AccordionDemoModulesAccessor GetModulesAccessor();
    }
}
