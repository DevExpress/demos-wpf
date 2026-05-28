using System;
using System.Windows.Data;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting.BarCode.Native;

namespace EditorsDemo {
    public class BarCodeSymbologyCoverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return BarCodeStyleSettingsStorage.Create((BarCodeSymbology)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((BarCodeStyleSettings)value).GeneratorBase.SymbologyCode;
        }
    }
}
