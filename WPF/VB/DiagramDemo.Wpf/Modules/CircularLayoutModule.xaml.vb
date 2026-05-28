Imports DevExpress.Xpf.Diagram

Namespace DiagramDemo

    Public Partial Class CircularLayoutModule
        Inherits LayoutModuleBase

        Protected Overrides ReadOnly Property Diagram As DiagramControl
            Get
                Return diagramControl
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            MainWindow.InitCommonRibbon(diagramControl)
            diagramControl.LoadDemoData("CircularLayoutDiagram.xml")
        End Sub

        Protected Overrides Sub RelayoutDiagramCore()
            diagramControl.ApplyCircularLayout()
        End Sub
    End Class
End Namespace
