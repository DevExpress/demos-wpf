Imports DevExpress.Xpf.RichEdit

Namespace RichEditDemo

    Public Partial Class Navigation
        Inherits RichEditDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(richEdit)
            MainWindow.CommonRibbon.RibbonCategory_HeaderFooterTools = False
            MainWindow.CommonRibbon.RibbonCategory_TableTools = False
            MainWindow.CommonRibbon.RibbonCategory_PictureTools = False
            MainWindow.CommonRibbon.RibbonPage_PageLayout = False
            MainWindow.CommonRibbon.RibbonPage_Review = False
            MainWindow.CommonRibbon.RibbonPage_View = False
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_MailMerge
        End Sub
    End Class
End Namespace
