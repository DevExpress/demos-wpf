Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PrintMasterDetailViewModel.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PrintViewModelBase.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/PrintDemoModuleResources.xaml")>
    Public Partial Class PrintingMasterDetail
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsPrintingMasterDetail
        End Sub
    End Class
End Namespace
