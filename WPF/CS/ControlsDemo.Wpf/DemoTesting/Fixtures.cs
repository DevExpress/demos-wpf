using ControlsDemo.Modules;
using DevExpress.Xpf.DemoBase.DemoTesting;
using System;
using System.Linq;

namespace ControlsDemo.Tests {
    public class ControlsCheckAllDemosFixture : CheckAllDemosFixture {
#if NET
        Type[] skipMemoryLeaksCheckModules = new Type[] { typeof(BreadcrumbSamplesModule) };
#else
        Type[] skipMemoryLeaksCheckModules = new Type[] { typeof(BreadcrumbSamplesModule), typeof(TaskbarServices) };
#endif
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
    }
}
