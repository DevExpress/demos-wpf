using DevExpress.Xpf.DemoBase;
using PivotGridDemo.Helpers;

namespace PivotGridDemo {
    public class ServerModeViewModel : ServerModeViewModelBase<OrderDataRecord> {
        #region DatabaseInfo
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(OrderDataContext.FileName, "OrderDataRecords", typeof(OrderDataRecord), DatabaseHelper.CreateValues);
        #endregion

        protected ServerModeViewModel()
            : base(DatabaseInfo, () => new OrderDataContext().OrderDataRecords) {
        }
    }
}
