Namespace GridDemo

    Public Partial Class UnboundColumns
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsUnboundColumns
        End Sub
    End Class
End Namespace
