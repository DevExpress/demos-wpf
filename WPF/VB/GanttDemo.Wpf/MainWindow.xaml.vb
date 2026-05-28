Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace GanttDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Printing)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As GanttDemoRibbon
            Get
                Return CType(Instance.Ribbon, GanttDemoRibbon)
            End Get
        End Property

        Public Sub New()
            Instance = Me
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
        End Sub

        Protected Overrides Sub OnHideDemoModule(ByVal demo As DemoModule)
            MyBase.OnHideDemoModule(demo)
            CommonRibbon.ExportGantt = Nothing
        End Sub
    End Class
End Namespace
