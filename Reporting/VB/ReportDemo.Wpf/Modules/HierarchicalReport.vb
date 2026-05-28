Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class HierarchicalReport
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.HierarchicalReport.Report()
        End Function
    End Class
End Namespace
