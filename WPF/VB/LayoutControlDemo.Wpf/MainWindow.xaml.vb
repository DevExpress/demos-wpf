Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace LayoutControlDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(False, True)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.Ribbon, PreloadCategories.LayoutControl, PreloadCategories.Docking, PreloadCategories.Accordion)
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
