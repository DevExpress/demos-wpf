Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class FallCatalog
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.NorthwindTraders.CatalogReport()
        End Function
    End Class
End Namespace
