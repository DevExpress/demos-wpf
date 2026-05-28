Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class SwissQRBill
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.SwissQRCode.SwissQRBill()
        End Function
    End Class
End Namespace
