Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/SmartColumnsGenerationViewModel.(cs)")>
    Public Partial Class SmartColumnsGeneration
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
        End Sub
    End Class
End Namespace
