Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class MasterDetail
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.MasterDetailReport.Report()
        End Function
    End Class
End Namespace
