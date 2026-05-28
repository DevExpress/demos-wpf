Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataUpdateAnimationViewModel.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/StockViewModelBase.(cs)")>
    Public Partial Class DataUpdateAnimation
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsDataUpdateAnimation
        End Sub
    End Class
End Namespace
