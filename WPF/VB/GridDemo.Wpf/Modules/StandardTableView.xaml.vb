Namespace GridDemo

    Public Partial Class StandardTableView
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsStandardTableView
        End Sub
    End Class
End Namespace
