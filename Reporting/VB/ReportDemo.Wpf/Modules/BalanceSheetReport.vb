Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class BalanceSheetReport
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.BalanceSheetReport.Report()
        End Function
    End Class
End Namespace
