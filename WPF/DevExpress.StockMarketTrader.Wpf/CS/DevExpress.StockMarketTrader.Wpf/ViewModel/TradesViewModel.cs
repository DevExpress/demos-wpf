using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using DevExpress.StockMarketTrader.Wpf.DataSources;

namespace DevExpress.StockMarketTrader.Wpf.ViewModel {
    public class TradesViewModel {
        public static TradesViewModel Create() {
            return ViewModelSource.Create(() => new TradesViewModel());
        }

        ObservableCollection<ClosedTransactionData> tradesDataSource = new ObservableCollection<ClosedTransactionData>();

        public ObservableCollection<ClosedTransactionData> TradesDataSource { get { return tradesDataSource; } }

        public void Init(List<ClosedTransactionData> closedTransactions) {
            foreach(ClosedTransactionData transaction in closedTransactions)
                tradesDataSource.Add(transaction);
        }
        public void UpdateData(ClosedTransactionData transaction) {
            tradesDataSource.Insert(0, transaction);
        }
    }
}
