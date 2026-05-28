Namespace DiagramDemo

    Public Partial Class FlowchartModule
        Inherits DiagramDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.InitCommonRibbon(diagramControl)
            diagramControl.LoadDemoData("Flowchart.xml")
        End Sub
    End Class
End Namespace
