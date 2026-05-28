using System;
using DevExpress.DemoData.Helpers;

namespace DevExpress.WindowsMailClient.Wpf.Internal {
    public class FilePathHelper {
        public static string GetFullPath(string name) {
            return DataFilesHelper.FindFile(name, "Data");
        }
    }
}
