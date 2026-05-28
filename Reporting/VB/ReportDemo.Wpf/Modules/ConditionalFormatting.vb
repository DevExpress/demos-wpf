Imports DevExpress.XtraReports

Namespace ReportWpfDemo

    Public Class ConditionalFormatting
        Inherits ReportModuleViewModelBase

        Protected Overrides Function CreateReport() As IReport
            Return New XtraReportsDemos.FormattingRules.Report()
        End Function
    End Class
End Namespace
