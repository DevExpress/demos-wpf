Namespace PivotGridDemo

    Public Partial Class CellTemplates
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportPivotGrid = pivotGrid
        End Sub
    End Class

    Public Enum CellTemplateType
        None
        ShareOnly
        ValueAndShare
    End Enum
End Namespace
