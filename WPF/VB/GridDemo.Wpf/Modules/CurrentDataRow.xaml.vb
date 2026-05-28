Namespace GridDemo

    Public Partial Class CurrentDataRow
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsCurrentDataRow
        End Sub
    End Class
End Namespace
