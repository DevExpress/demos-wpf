using DevExpress.Xpf.RichEdit;

namespace RichEditDemo {
    public partial class Comments : RichEditDemoModule {
        public Comments() {
            InitializeComponent();

            MainWindow.CommonRibbon.Init(richEdit);
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = false;
            MainWindow.CommonRibbon.RibbonCategory_TableTools = false;
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = false;
            MainWindow.CommonRibbon.RibbonPage_PageLayout = false;
            MainWindow.CommonRibbon.RibbonPage_References = false;
            MainWindow.CommonRibbon.RibbonPage_MailMerge = false;
            MainWindow.CommonRibbon.RibbonPage_View = false;
            MainWindow.CommonRibbon.RibbonPage_Insert = false;
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Review;
        }
    }
}
