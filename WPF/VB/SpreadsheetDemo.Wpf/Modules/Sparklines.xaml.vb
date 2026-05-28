Imports DevExpress.Xpf.Spreadsheet

Namespace SpreadsheetDemo

    Public Partial Class Sparklines
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(spreadsheetControl1)
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home
        End Sub
    End Class
End Namespace
