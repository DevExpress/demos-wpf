using System;
using System.Windows.Media;
using DevExpress.Images;
using DevExpress.Utils;
using DevExpress.Xpf.Core;

namespace DevExpress.WindowsMailClient.Wpf.Internal {
    public static class ImageSourceHelper {
        public static Uri CreateUri(string name, UriKind uriKind = UriKind.Absolute) {
            if(string.IsNullOrEmpty(name))
                return null;
            return new Uri("pack://application:,,,/DevExpress.WindowsMailClient.Wpf;component/Images/Mail/" + name, uriKind);
        }
        public static Uri GetDXImageUri(string path) {
#pragma warning disable DX0025 
            return AssemblyHelper.GetResourceUri(typeof(DXImages).Assembly, string.Format("Office2013/{0}.png", path));
#pragma warning restore DX0025 
        }
        public static ImageSource CreateSvgImageSource(string glyph) {
            return (ImageSource)new SvgImageSourceExtension { Uri = CreateUri(glyph, UriKind.RelativeOrAbsolute) }.ProvideValue(null);
        }
    }
}
