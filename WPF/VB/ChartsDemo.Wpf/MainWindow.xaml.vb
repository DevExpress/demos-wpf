Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace ChartsDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, True)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Charts)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As ChartsDemoRibbon
            Get
                Return CType(Instance.Ribbon, ChartsDemoRibbon)
            End Get
        End Property

        Public Sub New()
            Instance = Me
            DataContext = ViewModelSource.Create(Function() New MainViewModel())
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnHideDemoModule(ByVal demo As DemoModule)
            MyBase.OnHideDemoModule(demo)
            TryCast(demo, ChartsDemoModule)?.OnStopModule()
            Call CommonRibbon.Clear()
        End Sub

        Protected Overrides Sub OnDemoModuleCompletelyLoaded(ByVal demo As DemoModule)
            MyBase.OnDemoModuleCompletelyLoaded(demo)
            TryCast(demo, ChartsDemoModule)?.OnStartModule()
        End Sub
    End Class

    Public Class MainViewModel
        Inherits ViewModelBase

    End Class
End Namespace
