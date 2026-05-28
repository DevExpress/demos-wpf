using System.Diagnostics;
using System.IO;
using System.Windows;
using DevExpress.Data.Utils;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Security;

namespace MVVMDemo.Services {
    public class UIObjectServiceViewModel {
        public void Export() {
            const string format = "pdf";
            var path = SafePath.GetTempFileName(format);

            IUIObjectService service = this.GetRequiredService<IUIObjectService>("GridService");
            using(FileStream file = File.Create(path)) {
                service.Object.View.ExportToPdf(file);
            }
            if(MessageBox.Show("Do you want to open the following file?\r\n" + path, "Open File", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                SafeProcess.Start(path, null, startInfo => { startInfo.UseShellExecute = true; });
            else
                File.Delete(path);
        }

        public void ShowColumnChooser() {
            IUIObjectService service = this.GetRequiredService<IUIObjectService>("TableViewService");
            service.Object.ShowColumnChooser();
        }
    }
}
