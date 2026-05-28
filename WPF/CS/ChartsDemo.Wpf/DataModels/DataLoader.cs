using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Utils;

namespace ChartsDemo {
    static class DataLoader {
        public static XDocument LoadXmlFromResources(string fileName) {
            try {
                return SafeXml.CreateXDocument(LoadFromResources(fileName));
            }
            catch {
                return null;
            }
        }
        public static Stream LoadFromResources(string fileName) {
            try {
                Uri uri = new Uri("/ChartsDemo;component" + fileName, UriKind.RelativeOrAbsolute);
                return Application.GetResourceStream(uri).Stream;
            }
            catch {
                return null;
            }
        }
    }
}
