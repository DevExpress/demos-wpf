Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataAwareExport/ProductSaleData.cs")>
    Public Partial Class DataAwareExport
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsDataAwareExport
        End Sub
    End Class
End Namespace
