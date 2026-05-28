Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace PivotGridDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Charts, PreloadCategories.Printing)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As PivotGridDemoRibbon
            Get
                Return CType(Instance.Ribbon, PivotGridDemoRibbon)
            End Get
        End Property

        Public Sub New()
            Instance = Me
#If NET
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance)
#End If
            InitializeComponent()
        End Sub

        Protected Overrides Sub OnHideDemoModule(ByVal demo As DemoModule)
            MyBase.OnHideDemoModule(demo)
            CommonRibbon.ExportPivotGrid = Nothing
        End Sub
    End Class
End Namespace
