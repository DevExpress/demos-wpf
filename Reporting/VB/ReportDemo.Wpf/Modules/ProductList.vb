Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class ProductList
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.NorthwindTraders.ProductListReport()
        End Function
    End Class
End Namespace
