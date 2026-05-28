Namespace GridDemo

    Public Partial Class GridSummaries
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsGridSummaries
        End Sub
    End Class
End Namespace
