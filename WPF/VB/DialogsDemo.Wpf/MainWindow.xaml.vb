Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace DialogsDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Dialogs)
        End Sub

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
