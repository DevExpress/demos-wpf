Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class InteractiveSorting
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.InteractiveSorting.Report()
        End Function
    End Class
End Namespace
