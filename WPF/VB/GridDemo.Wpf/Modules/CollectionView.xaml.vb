Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CollectionViewViewModel.(cs)")>
    Public Partial Class CollectionView
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.ExportGrid = grid
            MainWindow.CommonRibbon.ReportService = rmsCollectionView
        End Sub
    End Class
End Namespace
