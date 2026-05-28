Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Grid

Namespace TreeListDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.RichEdit, PreloadCategories.Spreadsheet, PreloadCategories.Printing)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared Sub InitRibbon(ByVal treeList As TreeListControl)
            Dim ribbon = CType(Instance.Ribbon, TreeListDemoRibbon)
            ribbon.ExportTreeList = treeList
        End Sub

        Public Sub New()
            Instance = Me
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
        End Sub
    End Class
End Namespace
