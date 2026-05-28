Imports DevExpress.Xpf.Spreadsheet

Namespace SpreadsheetDemo

    Public Partial Class Hyperlinks
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(spreadsheetControl1)
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Insert
        End Sub
    End Class
End Namespace
