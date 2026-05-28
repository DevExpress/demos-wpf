using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo {
    public partial class DocumentProtection : RichEditDemoModule {
        readonly UserService userService = new UserService();

        public DocumentProtection() {
            InitializeComponent();

            MainWindow.CommonRibbon.Init(richEdit);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_MailMerge = false;
            MainWindow.CommonRibbon.RibbonPage_File = false;
            MainWindow.CommonRibbon.RibbonPage_Home = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.RibbonGroup_Review_Proofing = false;

            richEdit.ReplaceService(this.userService);
        }

        void RichEditControl_Loaded(object sender, RoutedEventArgs e) {
            richEdit.DocumentLoaded += RichEditControl_DocumentLoaded;
            richEdit.DocumentSource = DemoUtils.GetRelativePath("DocumentProtection.docx");
        }
        void RichEditControl_DocumentLoaded(object sender, EventArgs e) {
            RangePermissionCollection rangePermissions = richEdit.Document.BeginUpdateRangePermissions();
            richEdit.Document.CancelUpdateRangePermissions(rangePermissions);
            List<String> users = rangePermissions
                .Select(rangePermission => rangePermission.UserName)
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList();
            userService.Update(users);
            cbUserName.ItemsSource = users;
            if(users.Count > 0)
                cbUserName.EditValue = users[0];
        }
    }
}
