Namespace PivotGridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FilterViewModel.(cs)")>
    Public Partial Class Filter
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid
        End Sub
    End Class
End Namespace
