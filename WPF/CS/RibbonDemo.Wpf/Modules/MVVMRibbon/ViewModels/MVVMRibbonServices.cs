using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Ribbon;

namespace RibbonDemo {
    public interface IRibbonMergeingService {
        void Remerge();
    }
    public class RibbonMergeingService : ServiceBaseGeneric<RibbonControl>, IRibbonMergeingService {

        public void Remerge() {
            ((IRibbonControl)(AssociatedObject.ActualMergedParent ?? AssociatedObject)).ReMerge();
        }
    }
}
