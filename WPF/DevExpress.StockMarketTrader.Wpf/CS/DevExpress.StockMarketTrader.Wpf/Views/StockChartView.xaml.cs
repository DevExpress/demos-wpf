using System.Windows.Controls;
using DevExpress.Xpf.Charts;

namespace DevExpress.StockMarketTrader.Wpf.Views {
    public partial class StockChartView : UserControl {

        public ChartControl Chart { get { return chartControl; } }

        public StockChartView() {
            InitializeComponent();
        }
    }
}
