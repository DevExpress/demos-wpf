using DevExpress.Mvvm;

namespace TreeListDemo {
    public partial class BuildTreeFromSelfReferenceData : TreeListDemoModule {
        public BuildTreeFromSelfReferenceData() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
        }
        void OnLoaded(object sender, System.Windows.RoutedEventArgs e) {
            this.treeList.View.ExpandAllNodes();
        }
    }

    public class SelfReferenceDataViewModel : BindableBase {
        bool showServiceColumns;

        public bool ShowServiceColumns {
            get { return showServiceColumns; }
            set { SetProperty(ref showServiceColumns, value, () => ShowServiceColumns); }
        }
        public string KeyFieldName { get { return "Id"; } }
        public string ParentFieldName { get { return "ParentId"; } }
    }
}
