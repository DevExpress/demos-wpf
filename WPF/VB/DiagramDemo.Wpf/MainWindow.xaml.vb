Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class MainWindow
        Inherits DemoShellWindow

        Private Shared _Instance As MainWindow

        Shared Sub New()
            DemoPreloadHelper.PreloadData(True, False)
            Call DemoPreloadHelper.PreloadThemeAsync(PreloadCategories.Core, PreloadCategories.LayoutControl, PreloadCategories.Ribbon, PreloadCategories.Docking, PreloadCategories.Accordion, PreloadCategories.Grid, PreloadCategories.PropertyGrid)
        End Sub

        Public Shared Property Instance As MainWindow
            Get
                Return _Instance
            End Get

            Private Set(ByVal value As MainWindow)
                _Instance = value
            End Set
        End Property

        Public Shared ReadOnly Property CommonRibbon As DiagramRibbonControl
            Get
                Return CType(Instance.Ribbon, DiagramRibbonControl)
            End Get
        End Property

        Public Shared Sub InitCommonRibbon(ByVal diagram As DiagramDesignerControl)
            Call DiagramControl.SetDiagram(CommonRibbon, diagram)
            If diagram Is Nothing Then
                CommonRibbon.Visibility = System.Windows.Visibility.Collapsed
                Call MergingProperties.SetElementMergingBehavior(CommonRibbon, ElementMergingBehavior.None)
            Else
                CommonRibbon.Visibility = System.Windows.Visibility.Visible
                Call MergingProperties.SetElementMergingBehavior(CommonRibbon, ElementMergingBehavior.InternalWithExternal)
            End If
        End Sub

        Public Sub New()
            Instance = Me
#If NET
            Dim factory As System.Data.Common.DbProviderFactory
            If Not System.Data.Common.DbProviderFactories.TryGetFactory("Microsoft.Data.Sqlite", factory) Then System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance)
#End If
            InitializeComponent()
        End Sub
    End Class
End Namespace
