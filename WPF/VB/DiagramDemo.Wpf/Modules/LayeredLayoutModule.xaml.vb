Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class LayeredLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            MainWindow.InitCommonRibbon(diagramControl)
            diagramControl.LoadDemoData("LayeredLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplySugiyamaLayout()
        End Sub
    End Class
End Namespace
