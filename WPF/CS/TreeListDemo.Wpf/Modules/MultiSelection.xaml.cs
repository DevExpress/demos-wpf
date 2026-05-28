using System;
using System.Windows.Data;
using DevExpress.Xpf.Grid;

namespace TreeListDemo {
    public partial class MultiSelection : TreeListDemoModule {
        public MultiSelection() {
            InitializeComponent();
            MainWindow.InitRibbon(treeList);
            treeList.SelectRange(0, 10);
        }
        protected override void Show() {
            base.Show();
            treeList.UpdateTotalSummary();
        }
    }
    #region Converters
    public class MultiSelectModeToBoolConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !object.Equals(value, MultiSelectMode.None);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? MultiSelectMode.Row : MultiSelectMode.None;
        }
        #endregion
    }
    #endregion
}
