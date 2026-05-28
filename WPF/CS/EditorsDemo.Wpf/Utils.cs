using System;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Editors;
using System.Collections.Generic;
using GridDemo;
using DevExpress.Xpf.Core;
using System.Globalization;
using DevExpress.Data.Internal;

namespace EditorsDemo.Utils {
    public class FormatWrapper {
        public FormatWrapper() { }
        public FormatWrapper(string name, string format) {
            FormatName = name;
            FormatString = format;
        }
        public string FormatName { get; set; }
        public string FormatString { get; set; }
    }
    public class BaseKindHelper<T> {
        public Array GetEnumMemberList() {
            return Enum.GetValues(typeof(T));
        }
    }
    public static class ImageHelper {
        public static ImageSource GetSvgImage(string imageName) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri($"pack://application:,,,/EditorsDemo;component/Images/{imageName}.svg"), Size = new System.Windows.Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
    }
    public class NullableToStringConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        const string NullString = "Null";

        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value == null ? NullString : value.ToString();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class DecimalToConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = parameter as Type;
            return target == null ? value : System.Convert.ChangeType(value, target, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return System.Convert.ToDecimal(value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    public class IConvertibleConverter : IValueConverter {
        public string ToType { get; set; }
        public string FromType { get; set; }

        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = SafeTypeResolver.GetType(ToType);
            return target == null ? value : System.Convert.ChangeType(value, target, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = SafeTypeResolver.GetType(FromType);
            return target == null ? value : System.Convert.ChangeType(value, target, culture);
        }

        #endregion
    }

    public class CategoryDataToImageSourceConverter : BytesToImageSourceConverter {
        static readonly Dictionary<string, object> cachedImages = new Dictionary<string, object>();

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            CategoryData categoryData = value as CategoryData;
            if(value == null || categoryData == null)
                return null;
            object val;
            if(cachedImages.TryGetValue(categoryData.Name, out val))
                return val;

            object image = base.Convert(categoryData.Picture, targetType, parameter, culture);
            cachedImages.Add(categoryData.Name, image);
            return image;
        }
    }
    public class CategoryNameToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var category = (string)value;
            if(string.IsNullOrEmpty(category))
                return null;
            category = category.Replace(" ", "").Replace("/", "");
            return ImageHelper.GetSvgImage(@"Products/" + category);
        }
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
