using System.Collections.Generic;


namespace TreeListDemo {
    public partial class BandedLayout : TreeListDemoModule {
        public BandedLayout() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
    }

    public class BandedViewViewModel {
        public IList<SpaceObjects> SpaceObjects { get { return SpaceObjectData.DataSource; } }
    }
}
