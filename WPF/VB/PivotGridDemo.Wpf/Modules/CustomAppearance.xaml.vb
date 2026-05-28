Namespace PivotGridDemo

    Public Partial Class CustomAppearance
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid
        End Sub
    End Class
End Namespace
