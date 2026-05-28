Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class SideBySideReports
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.SideBySideReports.EmployeeComparisonReport()
        End Function
    End Class
End Namespace
