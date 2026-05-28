Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace NavigationDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(False, True)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.PdfViewer)
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
