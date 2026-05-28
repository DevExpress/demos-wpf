Namespace DiagramDemo

    Public Partial Class ShapesModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.InitCommonRibbon(diagramControl)
        End Sub
    End Class
End Namespace
