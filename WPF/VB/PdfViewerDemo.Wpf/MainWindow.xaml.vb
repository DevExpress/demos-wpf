Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace PdfViewerDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Grid)
        End Sub

        Public Sub New()
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
        End Sub
    End Class
End Namespace
