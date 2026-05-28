using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Editors;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace BarsDemo.Tests {
    
    
    public class BarsCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] {
        };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
    }
    public class BarsDemoModulesAccessor : DemoModulesAccessor<BarsDemoModule> {
        public BarsDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public BarManager Manager { get { return DemoModule.Manager; } }
    }
}
