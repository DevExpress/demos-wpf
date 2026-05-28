Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class CalculatedFields
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.CalculatedFieldsReport.Report()
        End Function
    End Class
End Namespace
