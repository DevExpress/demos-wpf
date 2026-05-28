Namespace GridDemo

    Public Partial Class DataDependentDetails
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsDataDependentDetails
        End Sub
    End Class
End Namespace
