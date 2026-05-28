Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace DockingDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Charts, PreloadCategories.PropertyGrid, PreloadCategories.Map)
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
