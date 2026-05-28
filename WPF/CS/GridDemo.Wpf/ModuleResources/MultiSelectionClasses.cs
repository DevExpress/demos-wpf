using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace GridDemo {
    public struct Range {
        public string Text { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public override string ToString() {
            return Text;
        }
    }
    public class ProductIdToProductNameConverter : IValueConverter {
        static Dictionary<long, Product> products;

        static Dictionary<long, Product> Products {
            get { return products ?? (products = NWindContext.Create().Products.ToDictionary(p => p.ProductID)); }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            long id = (long)value;
            Product product = null;
            Products.TryGetValue(id, out product);
            return product?.ProductName ?? string.Empty;

        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
