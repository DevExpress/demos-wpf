Namespace PivotGridDemo

    Public Partial Class FilterPopup
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid
        End Sub
    End Class
End Namespace
