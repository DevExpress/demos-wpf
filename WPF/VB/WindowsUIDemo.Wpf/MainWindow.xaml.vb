Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace WindowsUIDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.RichEdit)
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
