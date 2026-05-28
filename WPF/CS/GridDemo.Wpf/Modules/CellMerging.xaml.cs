namespace GridDemo {
    public partial class CellMerging : GridDemoModule {
        public CellMerging() {
            InitializeComponent();
            MainWindow.CommonRibbon.ExportGrid = grid;
        }
    }
}
