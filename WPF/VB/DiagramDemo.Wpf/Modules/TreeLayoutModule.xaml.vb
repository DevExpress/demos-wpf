Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class TreeLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            MainWindow.InitCommonRibbon(diagramControl)
            diagramControl.LoadDemoData("TreeLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyTreeLayout()
        End Sub
    End Class
End Namespace
