Namespace GridDemo

    Public Partial Class MasterDetailView
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsMasterDetailView
        End Sub
    End Class
End Namespace
