using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using DevExpress.Data.Utils;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;


namespace SpreadsheetDemo {
    public static class DocumentLoadHelper {
        public static Stream GetDocument(string path) {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(path);
        }
    }
    #region DemoUtils
    public class DemoUtils {
        public static string GetRelativePath(string fileName) {
            fileName = Path.Combine("Data", fileName);
            return DataFilesHelper.GetRelativeFilePath(fileName);
        }

        public static Stream GetDataStream(string fileName) {
            string path = DemoUtils.GetRelativePath(fileName);
            if(!String.IsNullOrEmpty(path)) {
                FileAccess fileAccess = (File.GetAttributes(path) & FileAttributes.ReadOnly) != 0 ? FileAccess.Read : FileAccess.ReadWrite;
                return new FileStream(path, FileMode.Open, fileAccess);
            }
            return null;
        }

        public static void SetDatabasePath() {
            const string dbName = "nwind.mdb";
            const string pathToDbTag = "|pathToDb|";
            string path = GetRelativePath(dbName);
            if(String.IsNullOrEmpty(path))
                return;
            string connectionString = global::SpreadsheetDemo.Properties.Settings.Default["nwindConnectionString"] as string;
            if(String.IsNullOrEmpty(connectionString))
                return;
            connectionString = connectionString.Replace(pathToDbTag, path);
            global::SpreadsheetDemo.Properties.Settings.Default["nwindConnectionString"] = connectionString;
        }
    }
    #endregion
    #region DialogHelper
    public class DialogHelper {
        public static MessageBoxResult ShowQuestionDialog(string message) {
            return ThemedMessageBox.Show(System.Windows.Forms.Application.ProductName, message, MessageBoxButton.OKCancel, MessageBoxImage.Question);
        }
        public static void ShowErrorDialog(string message) {
            ThemedMessageBox.Show(System.Windows.Forms.Application.ProductName, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    #endregion
    #region FileOpenHelper
    public class FileOpenHelper {
        public static void ShowFile(string fileName) {
            if(!File.Exists(fileName))
                return;

#if NET
            SafeProcess.Start(fileName, null, startInfo => { startInfo.UseShellExecute = true; });
#else
            SafeProcess.Start(fileName);
#endif
        }
    }
    #endregion
    #region FileSaveHelper
    public class FileSaveHelper {
        public static string GetSaveFileName(string filter, string defaultName) {
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = filter;
            sfDialog.FileName = defaultName;
            if(sfDialog.ShowDialog() != DialogResult.OK)
                return String.Empty;
            return sfDialog.FileName;
        }
    }
    #endregion
}
