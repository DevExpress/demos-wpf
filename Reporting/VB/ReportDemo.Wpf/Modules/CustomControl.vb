Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class CustomControl
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.CustomDraw.Report()
        End Function
    End Class
End Namespace
