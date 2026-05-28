Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class Subreports
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.Subreports.MasterReport()
        End Function
    End Class
End Namespace
