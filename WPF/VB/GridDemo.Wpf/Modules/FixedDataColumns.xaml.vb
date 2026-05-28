Namespace GridDemo

    Public Partial Class FixedDataColumns
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsFixedDataColumns
        End Sub
    End Class
End Namespace
