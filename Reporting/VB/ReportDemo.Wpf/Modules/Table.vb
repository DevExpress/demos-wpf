Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class Table
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.TableReport.Report()
        End Function
    End Class
End Namespace
