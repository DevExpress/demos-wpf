Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class MultiColumn
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.MultiColumnReport.Report()
        End Function
    End Class
End Namespace
