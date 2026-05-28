Imports System
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Spreadsheet

Namespace SpreadsheetDemo

    Public Partial Class ConditionalFormatting
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            MainWindow.CommonRibbon.Init(spreadsheetControl1)
            MainWindow.CommonRibbon.SelectedPage = DefaultBarItemNames.RibbonPage_Home
        End Sub

        Private Sub spreadsheetControl1_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            UpdateConditionalFormatting()
        End Sub

        Private Sub OnCheckedChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            UpdateConditionalFormatting()
        End Sub

        Private Sub UpdateConditionalFormatting()
            If spreadsheetControl1 Is Nothing Then Return
            spreadsheetControl1.BeginUpdate()
            Try
                Dim sheet As Worksheet = spreadsheetControl1.ActiveWorksheet
                sheet.ConditionalFormattings.Clear()
                If chkImports.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.ApplyTopImportsConditionalFormatting(sheet)
                    DevExpress.XtraSpreadsheet.Demos.ApplyImportsYearlyChangeConditionalFormatting(sheet)
                End If

                If chkExports.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.ApplyTopExportsConditionalFormatting(sheet)
                    DevExpress.XtraSpreadsheet.Demos.ApplyExportsYearlyChangeConditionalFormatting(sheet)
                End If

                If chkBalance.IsChecked = True Then
                    DevExpress.XtraSpreadsheet.Demos.ApplyBalanceTrendConditionalFormatting(sheet)
                    DevExpress.XtraSpreadsheet.Demos.ApplyBalanceChangeConditionalFormatting(sheet)
                End If

                If chkAsiaRegion.IsChecked = True Then DevExpress.XtraSpreadsheet.Demos.ApplyAsiaCountriesConditionalFormatting(sheet)
            Finally
                spreadsheetControl1.EndUpdate()
            End Try
        End Sub
    End Class
End Namespace
