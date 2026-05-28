Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/FixedRowsViewModel.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/StockViewModelBase.(cs)")>
    Public Partial Class FixedRows
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsFixedRows
        End Sub
    End Class
End Namespace
