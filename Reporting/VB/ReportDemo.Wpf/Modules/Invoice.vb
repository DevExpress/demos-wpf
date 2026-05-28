Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class Invoice
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.NorthwindTraders.InvoiceReport()
        End Function
    End Class
End Namespace
