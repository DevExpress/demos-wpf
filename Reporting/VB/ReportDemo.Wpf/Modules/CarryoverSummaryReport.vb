Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class CarryoverSummaryReport
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.CarryoverSummaryReport.Report()
        End Function
    End Class
End Namespace
