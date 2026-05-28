Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class IListDataSource
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.IListDataSource.Report()
        End Function
    End Class
End Namespace
