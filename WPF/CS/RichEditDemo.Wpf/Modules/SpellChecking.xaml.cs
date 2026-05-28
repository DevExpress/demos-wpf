using System.Globalization;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.SpellChecker;

namespace RichEditDemo {
    public partial class SpellChecking : RichEditDemoModule {
        public SpellChecking() {
            InitializeComponent();

            MainWindow.CommonRibbon.Init(richEdit);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_MailMerge = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.RibbonGroup_Review_Protect = false;
            MainWindow.CommonRibbon.RibbonGroup_Review_Comment = false;
            MainWindow.CommonRibbon.RibbonGroup_Review_Tracking = false;
            MainWindow.CommonRibbon.RibbonGroup_Review_Changes = false;
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Review;
        }
    }
}
