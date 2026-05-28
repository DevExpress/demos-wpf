using System.Reflection;
using DevExpress.Office.Utils;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Services;

namespace RichEditDemo {
    public partial class AutoCorrect : RichEditDemoModule {
        public AutoCorrect() {
            InitializeComponent();

            MainWindow.CommonRibbon.Init(richEdit);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_MailMerge = false;
            MainWindow.CommonRibbon.RibbonPage_Review = false;
        }

        void RichEditControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            IAutoCorrectService service = richEdit.GetService<IAutoCorrectService>();
            if(service != null) {
                AutoCorrectReplaceInfoCollection replaceTable = new AutoCorrectReplaceInfoCollection();
                replaceTable.Add("(C)", "©");
                Assembly assembly = GetType().Assembly;
                replaceTable.Add(new AutoCorrectReplaceInfo(":)", OfficeImage.CreateImage(assembly.GetManifestResourceStream(DemoHelper.GetPath("RichEditDemo.", assembly) + "smile.png"))));
                replaceTable.Add("pctus", "Please do not hesitate to contact us again in case of any further questions.");
                replaceTable.Add("wnwd", "well-nourished, well-developed");
                service.SetReplaceTable(replaceTable);
            }
        }
    }
}
