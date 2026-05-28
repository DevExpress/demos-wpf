Namespace GridDemo

    Public Partial Class DataGroupSummaries
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsDataGroupSummaries
        End Sub
    End Class
End Namespace
