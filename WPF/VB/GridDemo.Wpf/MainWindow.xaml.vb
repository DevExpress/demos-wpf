Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase

Namespace GridDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, True)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.Printing, PreloadCategories.RichEdit, PreloadCategories.Scheduling, PreloadCategories.Charts, PreloadCategories.Spreadsheet)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As GridDemoRibbon
            Get
                Return CType(Instance.Ribbon, GridDemoRibbon)
            End Get
        End Property

        Public Sub New()
            Instance = Me
            InitializeComponent()
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, True)
#If NET
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance)
#End If
        End Sub

        Protected Overrides Sub OnHideDemoModule(ByVal demo As DemoModule)
            MyBase.OnHideDemoModule(demo)
            CommonRibbon.ExportGrid = Nothing
            CommonRibbon.ReportService = Nothing
        End Sub
    End Class
End Namespace
