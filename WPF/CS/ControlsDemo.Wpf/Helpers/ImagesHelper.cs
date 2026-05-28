using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Data.Utils;

namespace CommonDemo.Helpers {
    public static class ImagesHelper {
        public static IEnumerable<ImageSource> LondonPhotos { get; private set; }
        public static ImageSource LondonPhoto1 { get { return LondonPhotos.ElementAt(0); } }
        public static ImageSource LondonPhoto2 { get { return LondonPhotos.ElementAt(1); } }
        public static ImageSource LondonPhoto3 { get { return LondonPhotos.ElementAt(2); } }
        public static ImageSource LondonPhoto4 { get { return LondonPhotos.ElementAt(3); } }
        public static ImageSource GetRandomLondonPhoto() {
            return LondonPhotos.ElementAt(NonCryptographicRandom.Default.Next(LondonPhotos.Count() - 1));
        }

        public static IEnumerable<ImageSource> NaturePhotos { get; private set; }
        public static ImageSource NaturePhoto1 { get { return NaturePhotos.ElementAt(0); } }
        public static ImageSource NaturePhoto2 { get { return NaturePhotos.ElementAt(1); } }
        public static ImageSource NaturePhoto3 { get { return NaturePhotos.ElementAt(2); } }
        public static ImageSource NaturePhoto4 { get { return NaturePhotos.ElementAt(3); } }
        public static ImageSource NaturePhoto5 { get { return NaturePhotos.ElementAt(4); } }
        public static ImageSource NaturePhoto6 { get { return NaturePhotos.ElementAt(5); } }
        public static ImageSource NaturePhoto7 { get { return NaturePhotos.ElementAt(6); } }
        public static ImageSource NaturePhoto8 { get { return NaturePhotos.ElementAt(7); } }
        public static ImageSource NaturePhoto9 { get { return NaturePhotos.ElementAt(8); } }
        public static ImageSource NaturePhoto10 { get { return NaturePhotos.ElementAt(9); } }
        public static ImageSource GetRandomNaturePhoto() {
            return NaturePhotos.ElementAt(NonCryptographicRandom.Default.Next(NaturePhotos.Count() - 1));
        }

        public static ImageSource GroupIcon { get; private set; }

        public static ImageSource GetWebIcon(string icon) {
            return new BitmapImage(new Uri(@"/ControlsDemo;component/Images/TabControl/" + icon, UriKind.Relative));
        }
        public static ImageSource GetSvgImage(string imagePath, Size imageSize) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri(imagePath), Size = imageSize };
            return (ImageSource)extension.ProvideValue(null);
        }
        static ImagesHelper() {
            string londonUriPath = @"/ControlsDemo;component/Images/Photos/London/";
            Func<string, ImageSource> getLondonImage = x => new BitmapImage(new Uri(londonUriPath + x, UriKind.Relative));
            var londonImages = new List<ImageSource> {
                getLondonImage("(London)-St-Paul's-Cathedral.jpg"),
                getLondonImage("(London)-Tower-Bridge.jpg"),
                getLondonImage("(London)-Tower-of-London.jpg"),
                getLondonImage("(London)-Trafalgar-Square.jpg"),
                getLondonImage("(London)-Westminster-Abbey.jpg")
            };
            LondonPhotos = londonImages;

            string natureUriPath = @"/ControlsDemo;component/Images/Photos/Nature/";
            Func<string, ImageSource> getNatureImage = x => new BitmapImage(new Uri(natureUriPath + x, UriKind.Relative));
            var naturePhotos = new List<ImageSource> {
                getNatureImage("01.JPG"),
                getNatureImage("02.JPG"),
                getNatureImage("03.JPG"),
                getNatureImage("04.JPG"),
                getNatureImage("05.JPG"),
                getNatureImage("06.JPG"),
                getNatureImage("07.JPG"),
                getNatureImage("08.JPG"),
                getNatureImage("09.JPG"),
                getNatureImage("10.JPG")
            };
            NaturePhotos = naturePhotos;

            GroupIcon = GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TabControl/GroupIcon.svg", new Size(24, 24));
        }
        
    }
}
