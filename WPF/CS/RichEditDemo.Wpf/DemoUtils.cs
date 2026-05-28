using System.IO;
using System.Windows;
using System.Windows.Forms;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.XtraRichEdit.Services;
using FormsApplication = System.Windows.Forms.Application;

namespace RichEditDemo {
    public class DemoUtils {
        public static string GetRelativePath(string fileName) {
            fileName = Path.Combine("Data", fileName);
            return DataFilesHelper.GetRelativeFilePath(fileName);
        }
        public static void ShowErrorMessage(string message) {
            ThemedMessageBox.Show(FormsApplication.ProductName, message, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    sealed class UserAccountService : IUserAccountService {
        static string userName;
        string IUserAccountService.GetUserName() {
            return userName ?? (userName = GetCurrentUserName());
        }
        string GetCurrentUserName() {
            try { return SystemInformation.UserName; }
            catch { return null; }
        }
    }
}
