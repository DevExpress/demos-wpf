using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Utils;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services {
    public class OpenFolderDialogServiceViewModel {
        public virtual string OpenedFolders { get; protected set; }
        public void ShowDialog() {
            IOpenFolderDialogService service = this.GetService<IOpenFolderDialogService>();
            string directory = SafeEnvironment.GetUserFolderPath(Environment.SpecialFolder.MyDocuments);
            if(service.ShowDialog(null, directory)) {
                IEnumerable<string> folderNames = service.Folders.Select(folderInfo => folderInfo.Path);
                OpenedFolders = string.Join(Environment.NewLine, folderNames);
            }
        }
    }
}
