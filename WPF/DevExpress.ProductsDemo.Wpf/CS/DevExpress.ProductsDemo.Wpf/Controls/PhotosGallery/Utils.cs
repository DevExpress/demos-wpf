using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Xml.Linq;
using DevExpress.Utils;

namespace ProductsDemo {
    public class DemoValuesProvider {
        public const string DevExpressAzureKey = "";
    }

    public static class DataLoader {
        public static XDocument LoadXmlFromResources(string fileName) {
            try {
#pragma warning disable DX0025 
                fileName = "/DevExpress.ProductsDemo.Wpf;component" + fileName;
#pragma warning restore DX0025
                Uri uri = new Uri(fileName, UriKind.RelativeOrAbsolute);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                return SafeXml.CreateXDocument(info.Stream);
            }
            catch {
                return null;
            }
        }
        public static XDocument LoadXmlFromFile(string fileName) {
            try {
                string xmlContent = File.ReadAllText(fileName);
                return SafeXml.CreateXDocument(xmlContent);
            }
            catch {
                return null;
            }
        }
    }

    public class PhotoGalleryResources {
        public BitmapImage CityInformationControlSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute)); } }
        public BitmapImage LabelControlImageSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute)); } }
        public BitmapImage PlaceInfoControlPrevImageSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/PrevPlace.png", UriKind.RelativeOrAbsolute)); } }
        public BitmapImage PlaceInfoControlNextImageSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/NextPlace.png", UriKind.RelativeOrAbsolute)); } }

        public PhotoGalleryResources() {
        }
    }
}
