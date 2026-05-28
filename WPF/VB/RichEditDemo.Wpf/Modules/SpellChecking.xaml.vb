Imports DevExpress.Xpf.RichEdit

Namespace RichEditDemo

    Public Partial Class SpellChecking
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(richEdit)
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = False
            MainWindow.CommonRibbon.RibbonCategory_TableTools = False
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = False
            MainWindow.CommonRibbon.RibbonPage_Insert = False
            MainWindow.CommonRibbon.RibbonPage_PageLayout = False
            MainWindow.CommonRibbon.RibbonPage_References = False
            MainWindow.CommonRibbon.RibbonPage_MailMerge = False
            MainWindow.CommonRibbon.RibbonPage_View = False
            MainWindow.CommonRibbon.RibbonGroup_Review_Protect = False
            MainWindow.CommonRibbon.RibbonGroup_Review_Comment = False
            MainWindow.CommonRibbon.RibbonGroup_Review_Tracking = False
            MainWindow.CommonRibbon.RibbonGroup_Review_Changes = False
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Review
        End Sub
    End Class
End Namespace
