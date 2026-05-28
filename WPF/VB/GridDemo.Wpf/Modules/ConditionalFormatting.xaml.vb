Namespace GridDemo

    Public Partial Class ConditionalFormatting
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsConditionalFormatting
        End Sub
    End Class
End Namespace
