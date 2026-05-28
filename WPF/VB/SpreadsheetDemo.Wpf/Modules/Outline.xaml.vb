Imports DevExpress.Xpf.Spreadsheet

Namespace SpreadsheetDemo

    Public Partial Class Outline
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(spreadsheetControl1)
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Data
        End Sub
    End Class
End Namespace
