using System.Windows;
using DevExpress.Xpf.Grid;

namespace TreeListDemo {
    public partial class AdvancedPrintingOptions : PrintTreeListDemoModule {
        public AdvancedPrintingOptions() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
            DXTabControl = tabControl;
            Loaded += AdvancedPrintingOptions_Loaded;
        }

        private void AdvancedPrintingOptions_Loaded(object sender, RoutedEventArgs e) {
            Loaded -= AdvancedPrintingOptions_Loaded;
            ExpandNodes();
            OnShowPrintPreviewInNewTab("TreeList Preview");
        }

        void ExpandNodes() {
            View.Nodes[0].IsExpanded = true;
            View.Nodes[0].Nodes[0].IsExpanded = true;
            View.Nodes[0].Nodes[1].IsExpanded = true;
        }
        void printStyleChooser_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(printStyleChooser.SelectedIndex > 0) {
                treeView.PrintColumnHeaderStyle = (Style)Resources["customPrintColumnHeaderStyle"];
                treeView.PrintCellStyle = (Style)Resources["customPrintCellStyle"];
                treeView.PrintRowIndentStyle = (Style)Resources["customIndentStyle"];
                treeView.PrintTotalSummaryStyle = (Style)Resources["customPrintTotalSummaryStyle"];
            }
            else {
                treeView.ClearValue(TreeListView.PrintColumnHeaderStyleProperty);
                treeView.ClearValue(TreeListView.PrintCellStyleProperty);
                treeView.ClearValue(TreeListView.PrintRowIndentStyleProperty);
                treeView.ClearValue(TreeListView.PrintTotalSummaryStyleProperty);
            }
        }
    }
}
