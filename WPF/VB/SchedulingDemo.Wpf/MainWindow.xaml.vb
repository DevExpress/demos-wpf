Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace SchedulingDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Scheduling, PreloadCategories.Printing)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As SchedulerDemoRibbon
            Get
                Return CType(Instance.Ribbon, SchedulerDemoRibbon)
            End Get
        End Property

        Public Sub New()
            Instance = Me
            InitializeComponent()
        End Sub
    End Class
End Namespace
